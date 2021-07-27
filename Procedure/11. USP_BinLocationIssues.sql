-- DROP PROCEDURE "USP_BinLocationIssues";

CALL "USP_BinLocationIssuesByBatch"('1IGSTIOD','BY001');

CREATE PROCEDURE "USP_BinLocationIssuesByBatch" (IN ItemCode nvarchar(100), IN BatchCode nvarchar(200)) 
AS 
	BEGIN
		DV20210722100557X67N_2VA1CDO_7A1=(
			SELECT 
				T0."BinAbs", T0."ItemCode", MAX(T0."OnHandQty") AS "IBQOnhandQty", 
				IFNULL(SUM(T2."OnHandQty"), 0) AS "OnHandQty", 'N' AS "BTNDistNumber", 
				'N' AS "BTNMnfSerial", 'N' AS "BTNLotNumber", 'N' AS "SRNDistNumber", 'N' AS "SRNMnfSerial", 
				'N' AS "SRNLotNumber", MIN(T5."AbsEntry") AS "AbsEntry", MIN(T1."BinCode") AS "BinCode", 10000044 AS "SnbType", 
				MIN(T5."AbsEntry") AS "BTNAbsEntry", MIN(T5."AbsEntry") AS "SRNAbsEntry", MIN(T1."WhsCode") AS "WhsCode" 
			FROM CAPTAINHOOK_PRD."OIBQ" T0 
				INNER JOIN CAPTAINHOOK_PRD."OBIN" T1 ON T0."BinAbs" = T1."AbsEntry" AND T0."OnHandQty" <> 0 
				LEFT OUTER JOIN CAPTAINHOOK_PRD."OBBQ" T2 ON T0."BinAbs" = T2."BinAbs" AND T0."ItemCode" = T2."ItemCode" AND T2."OnHandQty" <> 0 
				LEFT OUTER JOIN CAPTAINHOOK_PRD."OSBQ" T3 ON T0."BinAbs" = T3."BinAbs" AND T0."ItemCode" = T3."ItemCode" AND T3."OnHandQty" <> 0 
				LEFT OUTER JOIN CAPTAINHOOK_PRD."OBTN" T4 ON T2."SnBMDAbs" = T4."AbsEntry" AND T2."ItemCode" = T4."ItemCode" 
				LEFT OUTER JOIN CAPTAINHOOK_PRD."OSRN" T5 ON T3."SnBMDAbs" = T5."AbsEntry" AND T3."ItemCode" = T5."ItemCode" 
			WHERE T1."AbsEntry" >= (0) AND (T2."AbsEntry" IS NOT NULL) 
				AND T0."ItemCode" IN ((SELECT U0."ItemCode" FROM CAPTAINHOOK_PRD."OITM" U0 INNER JOIN CAPTAINHOOK_PRD."OITB" U1 ON U0."ItmsGrpCod" = U1."ItmsGrpCod" WHERE U0."ItemCode" IS NOT NULL AND U0."ItemCode" >= (:ItemCode) 
					AND U0."ItemCode" <= (:ItemCode))) AND T4."DistNumber" >= (:BatchCode) AND T4."DistNumber" <= (:BatchCode) 
			GROUP BY T0."BinAbs", T0."ItemCode" 
			
			UNION
			
			SELECT 
				T0."BinAbs", T0."ItemCode", MAX(T0."OnHandQty") AS "IBQOnhandQty", IFNULL(SUM(T3."OnHandQty"), 0) AS "OnHandQty", 
				'N' AS "BTNDistNumber", 'N' AS "BTNMnfSerial", 'N' AS "BTNLotNumber", 'N' AS "SRNDistNumber", 'N' AS "SRNMnfSerial", 
				'N' AS "SRNLotNumber", MIN(T4."AbsEntry") AS "AbsEntry", MIN(T1."BinCode") AS "BinCode", 10000045 AS "SnbType", 
				MIN(T4."AbsEntry") AS "BTNAbsEntry", MIN(T4."AbsEntry") AS "SRNAbsEntry", MIN(T1."WhsCode") AS "WhsCode" 
			FROM CAPTAINHOOK_PRD."OIBQ" T0 INNER JOIN CAPTAINHOOK_PRD."OBIN" T1 ON T0."BinAbs" = T1."AbsEntry" AND T0."OnHandQty" <> 0 
				LEFT OUTER JOIN CAPTAINHOOK_PRD."OBBQ" T2 ON T0."BinAbs" = T2."BinAbs" AND T0."ItemCode" = T2."ItemCode" AND T2."OnHandQty" <> 0 
				LEFT OUTER JOIN CAPTAINHOOK_PRD."OSBQ" T3 ON T0."BinAbs" = T3."BinAbs" AND T0."ItemCode" = T3."ItemCode" AND T3."OnHandQty" <> 0 
				LEFT OUTER JOIN CAPTAINHOOK_PRD."OBTN" T4 ON T2."SnBMDAbs" = T4."AbsEntry" AND T2."ItemCode" = T4."ItemCode" 
				LEFT OUTER JOIN CAPTAINHOOK_PRD."OSRN" T5 ON T3."SnBMDAbs" = T5."AbsEntry" AND T3."ItemCode" = T5."ItemCode" 
			WHERE T1."AbsEntry" >= (0) AND (T2."AbsEntry" IS NOT NULL) 
				AND T0."ItemCode" IN ((SELECT U0."ItemCode" FROM CAPTAINHOOK_PRD."OITM" U0 INNER JOIN CAPTAINHOOK_PRD."OITB" U1 ON U0."ItmsGrpCod" = U1."ItmsGrpCod" WHERE U0."ItemCode" IS NOT NULL AND U0."ItemCode" >= (:ItemCode) 
				AND U0."ItemCode" <= (:ItemCode))) AND T4."DistNumber" >= (:BatchCode) AND T4."DistNumber" <= (:BatchCode) 
			GROUP BY T0."BinAbs", T0."ItemCode");
		
		Create LOCAL TEMPORARY COLUMN TABLE #TB_DV20210722100557X67N_2VA1CDO_7A1 AS(Select * from :DV20210722100557X67N_2VA1CDO_7A1);
		
		SELECT 
			T0."BinAbs", T0."ItemCode", T2."OnHandQty", T4."DistNumber", T4."MnfSerial", T4."LotNumber", T5."DistNumber", T5."MnfSerial", 
			T5."LotNumber", T4."AbsEntry", T1."BinCode", T0."WhsCode" 
		FROM CAPTAINHOOK_PRD."OIBQ" T0 
			INNER JOIN CAPTAINHOOK_PRD."OBIN" T1 ON T0."BinAbs" = T1."AbsEntry" AND T0."OnHandQty" <> 0 
			LEFT OUTER JOIN CAPTAINHOOK_PRD."OBBQ" T2 ON T0."BinAbs" = T2."BinAbs" AND T0."ItemCode" = T2."ItemCode" AND T2."OnHandQty" <> 0 
			LEFT OUTER JOIN CAPTAINHOOK_PRD."OSBQ" T3 ON T0."BinAbs" = T3."BinAbs" AND T0."ItemCode" = T3."ItemCode" AND T3."OnHandQty" <> 0 
			LEFT OUTER JOIN CAPTAINHOOK_PRD."OBTN" T4 ON T2."SnBMDAbs" = T4."AbsEntry" AND T2."ItemCode" = T4."ItemCode" 
			LEFT OUTER JOIN CAPTAINHOOK_PRD."OSRN" T5 ON T3."SnBMDAbs" = T5."AbsEntry" AND T3."ItemCode" = T5."ItemCode" 
		WHERE T1."AbsEntry" >= (0) AND (T2."AbsEntry" IS NOT NULL) 
			AND T0."ItemCode" IN ((SELECT U0."ItemCode" FROM CAPTAINHOOK_PRD."OITM" U0 INNER JOIN CAPTAINHOOK_PRD."OITB" U1 ON U0."ItmsGrpCod" = U1."ItmsGrpCod" WHERE U0."ItemCode" IS NOT NULL AND U0."ItemCode" >= (:ItemCode) AND U0."ItemCode" <= (:ItemCode))) 
			AND T4."DistNumber" >= (:BatchCode) AND T4."DistNumber" <= (:BatchCode) 
		
		UNION
		
		SELECT 
			T0."BinAbs", T0."ItemCode", T3."OnHandQty", T4."DistNumber", T4."MnfSerial", T4."LotNumber", T5."DistNumber", T5."MnfSerial", 
			T5."LotNumber", T5."AbsEntry", T1."BinCode", T0."WhsCode" 
		FROM CAPTAINHOOK_PRD."OIBQ" T0 
			INNER JOIN CAPTAINHOOK_PRD."OBIN" T1 ON T0."BinAbs" = T1."AbsEntry" AND T0."OnHandQty" <> 0 
			LEFT OUTER JOIN CAPTAINHOOK_PRD."OBBQ" T2 ON T0."BinAbs" = T2."BinAbs" AND T0."ItemCode" = T2."ItemCode" AND T2."OnHandQty" <> 0 
			LEFT OUTER JOIN CAPTAINHOOK_PRD."OSBQ" T3 ON T0."BinAbs" = T3."BinAbs" AND T0."ItemCode" = T3."ItemCode" AND T3."OnHandQty" <> 0 
			LEFT OUTER JOIN CAPTAINHOOK_PRD."OBTN" T4 ON T2."SnBMDAbs" = T4."AbsEntry" AND T2."ItemCode" = T4."ItemCode" 
			LEFT OUTER JOIN CAPTAINHOOK_PRD."OSRN" T5 ON T3."SnBMDAbs" = T5."AbsEntry" AND T3."ItemCode" = T5."ItemCode" 
		WHERE T1."AbsEntry" >= (0) AND (T3."AbsEntry" IS NOT NULL) 
			AND T0."ItemCode" IN ((SELECT U0."ItemCode" FROM CAPTAINHOOK_PRD."OITM" U0 INNER JOIN CAPTAINHOOK_PRD."OITB" U1 ON U0."ItmsGrpCod" = U1."ItmsGrpCod" WHERE U0."ItemCode" IS NOT NULL AND U0."ItemCode" >= (:ItemCode) AND U0."ItemCode" <= (:ItemCode))) 
			AND T4."DistNumber" >= (:BatchCode) AND T4."DistNumber" <= (:BatchCode) 
			
		UNION
		
		SELECT 
			T0."BinAbs", T0."ItemCode", T0."OnHandQty", T4."DistNumber", T4."MnfSerial", T4."LotNumber", T5."DistNumber", T5."MnfSerial", 
			T5."LotNumber", T4."AbsEntry", T1."BinCode", T0."WhsCode" 
		FROM CAPTAINHOOK_PRD."OIBQ" T0 
			INNER JOIN CAPTAINHOOK_PRD."OBIN" T1 ON T0."BinAbs" = T1."AbsEntry" AND T0."OnHandQty" <> 0 
			LEFT OUTER JOIN CAPTAINHOOK_PRD."OBBQ" T2 ON T0."BinAbs" = T2."BinAbs" AND T0."ItemCode" = T2."ItemCode" AND T2."OnHandQty" <> 0 
			LEFT OUTER JOIN CAPTAINHOOK_PRD."OSBQ" T3 ON T0."BinAbs" = T3."BinAbs" AND T0."ItemCode" = T3."ItemCode" AND T3."OnHandQty" <> 0 
			LEFT OUTER JOIN CAPTAINHOOK_PRD."OBTN" T4 ON T2."SnBMDAbs" = T4."AbsEntry" AND T2."ItemCode" = T4."ItemCode" 
			LEFT OUTER JOIN CAPTAINHOOK_PRD."OSRN" T5 ON T3."SnBMDAbs" = T5."AbsEntry" AND T3."ItemCode" = T5."ItemCode" 
		WHERE T1."AbsEntry" >= (0) AND (T2."AbsEntry" IS NULL AND T3."AbsEntry" IS NULL) 
			AND T0."ItemCode" IN ((SELECT U0."ItemCode" FROM CAPTAINHOOK_PRD."OITM" U0 INNER JOIN CAPTAINHOOK_PRD."OITB" U1 ON U0."ItmsGrpCod" = U1."ItmsGrpCod" WHERE U0."ItemCode" IS NOT NULL AND U0."ItemCode" >= (:ItemCode) AND U0."ItemCode" <= (:ItemCode))) 
			AND T4."DistNumber" >= (:BatchCode) AND T4."DistNumber" <= (:BatchCode) 
		
		UNION
		
		SELECT 
			T0."BinAbs", T0."ItemCode", T0."IBQOnhandQty" - T0."OnHandQty", T0."BTNDistNumber", T0."BTNMnfSerial", T0."BTNLotNumber", 
			T0."SRNDistNumber", T0."SRNMnfSerial", T0."SRNLotNumber", T0."AbsEntry", T0."BinCode", T0."WhsCode" 
		FROM #TB_DV20210722100557X67N_2VA1CDO_7A1 T0 
		WHERE T0."IBQOnhandQty" > T0."OnHandQty" AND T0."BTNDistNumber" >= (:BatchCode) AND T0."BTNDistNumber" <= (:BatchCode);
		
		DROP TABLE #TB_DV20210722100557X67N_2VA1CDO_7A1;
	END;
