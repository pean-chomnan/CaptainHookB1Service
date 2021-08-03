-- DROP PROCEDURE CAPTAINHOOK_PRD."USP_LoadProductionOrderAvaibableIssueLine";

ALTER PROCEDURE CAPTAINHOOK_PRD."USP_LoadProductionOrderAvaibableIssueLine" (IN ProductionOrderEntry VARCHAR(1000)) 
AS 
	BEGIN 
		POEntry=(
			SELECT * FROM CAPTAINHOOK_PRD."SplitString"(:ProductionOrderEntry,',')
		);
		Create LOCAL TEMPORARY COLUMN TABLE #TB_POEntry AS(Select * from :POEntry);
		
		SELECT 
			T0."Series",
			T7."SeriesName",
			T0."DocEntry", T0."DocNum", 
			T2."DocItemCode" As "ItemCode", 
			T2."DocItemName" As "ItemName", 
			T2."DocItemType" As "ItemType",
			T1."wareHouse",  
			T1."LineNum", 
			T1."IssuedQty", 		
			T1."PlannedQty", 
			T1."PlannedQty"-T1."IssuedQty" As "AvaibleIssue",
			T6."OnHand",T5."IsCommited",
			T5."OnOrder",T6."OnHand"-T6."IsCommited" As "StockAvaible",
			T0."Type", 
			T1."StartDate", T1."EndDate", 
			T3."SeqNum", T4."Code", T3."Name",
			T0."OcrCode",
			T0."OcrCode2",
			T0."OcrCode3",
			T0."OcrCode4",
			T0."OcrCode5",
			IFNULL(T5."U_Expirydate",0) As "Expirydate"
		FROM CAPTAINHOOK_PRD."OWOR" T0 
			INNER JOIN CAPTAINHOOK_PRD."WOR1" T1 ON T0."DocEntry" = T1."DocEntry" 
			INNER JOIN CAPTAINHOOK_PRD."B1_DocItemView" T2 ON T1."ItemType" = T2."DocItemType" AND T1."ItemCode" = T2."DocItemCode" 
			LEFT OUTER JOIN CAPTAINHOOK_PRD."WOR4" T3 ON T1."StageId" = T3."StageId" AND T1."DocEntry" = T3."DocEntry" 
			LEFT OUTER JOIN CAPTAINHOOK_PRD."ORST" T4 ON T3."StgEntry" = T4."AbsEntry" 
			INNER JOIN CAPTAINHOOK_PRD."OITM" T5 ON T1."ItemCode"=T5."ItemCode" 
			INNER JOIN CAPTAINHOOK_PRD."OITW" T6 ON T1."ItemCode"=T6."ItemCode" AND T1."wareHouse"=T6."WhsCode"
			INNER JOIN CAPTAINHOOK_PRD."NNM1" T7 ON T0."Series"=T7."Series"
		WHERE T1."IssueType" = ('M') 
			AND T0."DocEntry" IN(SELECT VAL FROM #TB_POEntry) AND (((T0."Type" = ('S') OR T0."Type" = ('P')) 
			AND T1."PlannedQty" > T1."IssuedQty") OR (T0."Type" = ('D') AND T1."IssuedQty" > (0))) 
		ORDER BY T1."DocEntry", T1."VisOrder", T1."LineNum";
		
		DROP TABLE #TB_POEntry;
END;

 CALL CAPTAINHOOK_PRD."USP_LoadProductionOrderAvaibableIssueLine" ('14,15,16') ;


