ALTER PROCEDURE CAPTAINHOOK_PRD."USP_LoadIssueForProductionToReceiptFromProduction" 
AS 
	BEGIN 
		SELECT 
			T0."DocEntry", T0."DocNum", 
			T1."SeriesName", T0."Type",
			T0."PostDate", 
			T0."DueDate", 
			T0."ItemCode" As "ProductNo", 
			T0."ProdName", T0."Comments" ,IFNULL(T2."U_Expirydate",0) As "Expirydate"
		FROM CAPTAINHOOK_PRD."OWOR" T0 
			INNER JOIN CAPTAINHOOK_PRD."NNM1" T1 ON T0."Series" = T1."Series" 
			INNER JOIN "CAPTAINHOOK_PRD"."OITM" T2 ON T0."ItemCode"=T2."ItemCode"
		WHERE T0."Status" = ('R') AND (((T0."Type" = ('S') OR T0."Type" = ('P')) 
			AND EXISTS (SELECT U0."DocEntry" FROM CAPTAINHOOK_PRD."WOR1" U0 WHERE T0."DocEntry" = U0."DocEntry" AND U0."IssueType" = ('M') 
			AND U0."IssuedQty" > (0))) OR (T0."Type" = ('D') AND 
			EXISTS (SELECT U0."DocEntry" FROM CAPTAINHOOK_PRD."WOR1" U0 WHERE T0."DocEntry" = U0."DocEntry" 
			AND U0."IssueType" = ('M') AND U0."PlannedQty" > U0."IssuedQty"))) 
		ORDER BY T0."DocNum";
	END;
CALL CAPTAINHOOK_PRD."USP_LoadIssueForProductionToReceiptFromProduction" ;
