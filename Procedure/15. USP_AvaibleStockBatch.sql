--	drop procedure "CAPTAINHOOK_PRD".USP_StockAvaibleBatch;
	CALL "CAPTAINHOOK_PRD".USP_StockAvaibleBatch('1IGSG0000','03.HKT05','2021070610003');   --2021070610003   --2021070610001
  
ALTER PROCEDURE "CAPTAINHOOK_PRD".USP_StockAvaibleBatch (	
	  IN ItemCode nvarchar(1000)
	, IN WhsCode Nvarchar(50)
	, IN Batch nvarchar(100))
AS 
	DT Date;
BEGIN  
	
	SELECT TO_CHAR(CURRENT_DATE,'yyyy.mm.dd') INTO DT FROM DUMMY;
	
	TB_TMPINV=(   
		SELECT T1."CalcPrice" AS "OBVL_CalcPrice", T0."RowAction"
			, T0."ILMEntry", T0."ItemCode", T0."DistNumber", T0."ActionType"
			, T0."AccumType", T0."Quantity", T0."InvValue", T0."CalcPrice"
			, T0."CalcPrice" * T0."Quantity" AS "OBVL_CalcPriceTotal"
			, T0."TransValue", T0."AbsEntry" 
		FROM "CAPTAINHOOK_PRD"."OBVL" T0 
			LEFT OUTER JOIN "CAPTAINHOOK_PRD"."OBVL" T1 ON T0."AbsEntry" = T1."AbsEntry" AND T1."RowAction" = 2
		);	--TMPINV
	CREATE LOCAL TEMPORARY COLUMN TABLE #TMPINV AS(Select * from :TB_TMPINV);	
			
	TB_TMP_DATA=(  
		SELECT 
			  T0."DocDate",T1."ItemCode", T1."ItemName" 
			, T0."Warehouse"
			, T0."TransType"
			, T0."BASE_REF"
			, T0."CreatedBy"
			, T0."DocLineNum"
			, TO_DECIMAL(SUM(T0."InQty"),10,2) As "InQty"
			, TO_DECIMAL(SUM(T0."OutQty"),10,2) As "OutQty"
		FROM "CAPTAINHOOK_PRD"."OINM" T0 
			INNER JOIN "CAPTAINHOOK_PRD"."OITM" T1 ON T1."ItemCode" = T0."ItemCode" 
			LEFT OUTER JOIN "CAPTAINHOOK_PRD"."OIVK" T2 ON T2."INMTransSe" = T0."TransSeq" 
			LEFT OUTER JOIN "CAPTAINHOOK_PRD"."OIVL" T3 ON T3."TransSeq" = T2."TransSeq" 
			LEFT OUTER JOIN #TMPINV  T4 ON T4."ILMEntry" = T3."MessageID" 
				AND T4."ItemCode" = T1."ItemCode" AND T4."DistNumber" IS NOT NULL 
				AND (T4."ActionType" = 2 OR (T0."TransType" IN (10000071,310000001) 
				AND T4."ActionType" = 1)) AND T4."AccumType" = 1 
			LEFT OUTER JOIN "CAPTAINHOOK_PRD"."OWHS" T5 ON T5."WhsCode" = T0."Warehouse" 
			LEFT OUTER JOIN "CAPTAINHOOK_PRD"."OBPL" T6 ON T5."BPLid" = T6."BPLId" 
			
		WHERE  
			    T0."ItemCode"=ItemCode
			AND T0."Warehouse"=WhsCode
			AND (T0."TransValue" <> (0)OR T0."InQty" <> (0) OR T0."OutQty" <> (0) OR T0."TransType" = (162)) 
		GROUP BY  
			  T0."DocDate",T1."ItemCode", T1."ItemName" 
			, T0."Warehouse"
			, T0."TransType"
			, T0."BASE_REF"
			, T0."CreatedBy"
			, T0."DocLineNum"
			
		ORDER BY T1."ItemCode", T0."DocDate");
		
	CREATE LOCAL TEMPORARY COLUMN TABLE #TMP_DATA AS (Select * from :TB_TMP_DATA);	

	TB_TMP_Batch=(
		SELECT 
			  T0."ItemCode"
			, T0."ItemName"
			, T0."Warehouse"
			, B."BatchNum"
			, TO_DECIMAL(T0."InQty",10,2) As "InQty"
			, TO_DECIMAL(T0."OutQty",10,2) As "OutQty"
			, TO_DECIMAL(B."Quantity",10,2) As "Quantity"			
			, CASE WHEN TO_DECIMAL(((IFNULL(T0."InQty", 0)) + (IFNULL(T0."OutQty", 0))),10,2)>TO_DECIMAL((IFNULL(B."Quantity",0)))
				THEN 
					CASE WHEN TO_DECIMAL((IFNULL(T0."InQty", 0)))-(IFNULL(T0."OutQty", 0))>0 THEN
						TO_DECIMAL((IFNULL(B."Quantity",0))) 
					ELSE 
						-TO_DECIMAL((IFNULL(B."Quantity",0))) 
					END	
				ELSE 
					TO_DECIMAL(IFNULL(T0."InQty", 0),10,2) - TO_DECIMAL(IFNULL(T0."OutQty", 0),10,2)					
			  END As "TranQty"
		FROM #TMP_DATA  T0 
	         LEFT JOIN "CAPTAINHOOK_PRD"."OITM" T1 ON T1."ItemCode" = T0."ItemCode"
	         LEFT JOIN "CAPTAINHOOK_PRD".VBatchInfo B ON T0."ItemCode"=B."ItemCode" AND T0."BASE_REF"=B."AppDocNum" AND T0."CreatedBy"=B."ApplyEntry" And T0."DocLineNum"=B."DocLine" AND T0."TransType"=B."TransType"	 
		WHERE T0."DocDate" <= (:DT) AND T1."ManBtchNum"='Y' 
		ORDER BY T0."ItemCode"
	);
	
	CREATE LOCAL TEMPORARY COLUMN TABLE #TMP_Batch AS (Select * from :TB_TMP_Batch);	
	
--	SELECT * FROM #TMP_Batch;
	
	TB_TMP_Stock_Trans=(		
		SELECT 
			  T0."ItemCode", T0."ItemName"
			, T0."Warehouse"
			, T0."BatchNum"
			, SUM(T0."TranQty") As "TranQty"
		FROM #TMP_Batch  T0 			
		GROUP BY T0."ItemCode", T0."ItemName",T0."Warehouse", T0."BatchNum"
	);
		
	CREATE LOCAL TEMPORARY COLUMN TABLE #TMP_Stock_Trans AS(Select * from :TB_TMP_Stock_Trans);
	
	TB_tmp_RES_90_879=(SELECT * FROM #TMP_DATA WHERE 1 = 2);		
	CREATE LOCAL TEMPORARY COLUMN TABLE #tmp_RES_90_879 AS(Select * from :TB_tmp_RES_90_879);
			
	INSERT INTO #tmp_RES_90_879 (
		SELECT *
		FROM #TMP_DATA T0 			
		WHERE  T0."DocDate" <= (:DT) 
		ORDER BY T0."ItemCode", T0."DocDate");
		
	CREATE COLUMN TABLE TMP_ELBAT_PMET  AS (SELECT DISTINCT "ItemCode" FROM #tmp_RES_90_879);
	
	-- Final Result

	TB_FINAL=(
		SELECT    
			A."ItemCode"
			, A."ItemName"
			, D."WhsName"
			, D."WhsCode"
			, A."BatchNum"
			, IFNULL(A."TranQty", 0) AS "Quantity"
			, 1 As "Num"
		FROM #TMP_Stock_Trans A 	
			LEFT OUTER JOIN "CAPTAINHOOK_PRD"."OITM" C ON A."ItemCode"=C."ItemCode"
			LEFT OUTER JOIN "CAPTAINHOOK_PRD"."OWHS" D ON D."WhsCode"=A."Warehouse"
			LEFT OUTER JOIN "CAPTAINHOOK_PRD"."OITB" E ON E."ItmsGrpCod"=C."ItmsGrpCod"
		WHERE C."ManBtchNum"='Y'
	);
	CREATE LOCAL TEMPORARY COLUMN TABLE #TMP_FFF AS(Select * from :TB_FINAL);	
	
	SELECT 
		  ROW_NUMBER() OVER(ORDER BY T0."ItemCode") AS "#Row"
		, T0."ItemCode",T0."ItemName"
		, T0."BatchNum",T0."WhsCode"
		, T0."WhsName"
		, TO_DECIMAL(T0."Quantity",10,2) AS "Quantity"
		, "MnfSerial","LotNumber","ExpDate"
		, "MnfDate","Location","Notes","U_ACT_WeightOnBatch"
		, "U_CompanyAddress","U_BarCodeBoxNumber"
		, "U_SmokingSystem"
	FROM #TMP_FFF T0
		LEFT OUTER JOIN "CAPTAINHOOK_PRD"."OBTN" T1 ON T0."BatchNum"=T1."DistNumber"
	WHERE 
			T0."BatchNum"=Batch --"Quantity"<>0 
	ORDER BY T0."ItemCode";

	DROP TABLE #TMP_FFF;
	DROP TABLE #tmp_RES_90_879;
	DROP TABLE #TMP_DATA ;
	DROP TABLE TMP_ELBAT_PMET ;
	DROP TABLE #TMPINV ;
	DROP TABLE #TMP_Stock_Trans;
	DROP TABLE #TMP_Batch;
END;







