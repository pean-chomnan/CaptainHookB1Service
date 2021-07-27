-- Receipt From Production
ALTER PROCEDURE CAPTAINHOOK_PRD."USP_LoadProductionOrderAvaibableReceiptFromProduction" 
AS 
BEGIN 
	SELECT Distinct
		T0."DocEntry", T0."DocNum", 
		T1."SeriesName", T0."Type", 
		T0."PostDate",
		T0."DueDate", T0."ItemCode" As "ProductNo", 
		T0."ProdName", T0."Comments", 
		T0."StartDate", T0."Priority", 
		T0."Status",
		T0."Warehouse",
		T0."OcrCode",
		T0."OcrCode2",
		T0."OcrCode3",
		T0."OcrCode4",
		T0."OcrCode5",
		T0."PlannedQty",
		T0."PlannedQty"-T0."CmpltQty" As "AvaibleReceipt",
		T3."OnHand",
		T2."IsCommited",
		T2."OnOrder",
		T3."OnHand"-T3."IsCommited" As "StockAvaible",
		T0."CmpltQty",
		T0."RjctQty"
	FROM CAPTAINHOOK_PRD."OWOR" T0 
		INNER JOIN CAPTAINHOOK_PRD."NNM1" T1 ON T0."Series" = T1."Series" 
		INNER JOIN CAPTAINHOOK_PRD."OITM" T2 ON T0."ItemCode"=T2."ItemCode" 		
		INNER JOIN CAPTAINHOOK_PRD."OITW" T3 ON T0."ItemCode"=T3."ItemCode" AND T0."Warehouse"=T3."WhsCode"
	WHERE T0."Status" = ('R') AND (T0."Type" = ('S') OR T0."Type" = (n'P')) AND T0."PlannedQty" > T0."CmpltQty" 
	ORDER BY T0."DocNum";
END;

 CALL CAPTAINHOOK_PRD."USP_LoadProductionOrderAvaibableReceiptFromProduction" ;

CALL CAPTAINHOOK_PRD."USP_LoadProductionOrderAvaibableReceiptFromProduction"()
