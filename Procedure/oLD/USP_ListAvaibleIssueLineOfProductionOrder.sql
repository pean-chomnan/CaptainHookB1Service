-- DROP PROCEDURE CAPTAINHOOK_PRD."USP_ListAvaibleIssueLineOfProductionOrder";

ALTER PROCEDURE CAPTAINHOOK_PRD."USP_ListAvaibleIssueLineOfProductionOrder"(IN ProductionOrderEntry VARCHAR(500))
AS
BEGIN
	POEntry=(
		SELECT * FROM CAPTAINHOOK_PRD."SplitString"(:ProductionOrderEntry,',')
	);
	
	Create LOCAL TEMPORARY COLUMN TABLE #TB_POEntry AS(Select * from :POEntry);

--	SELECT * FROM #TB_POEntry;

	SELECT 
		A."DocEntry",A."DocNum",A."Series",B."SeriesName",
		CASE A."Type" WHEN 'S' THEN 'Standard' WHEN 'P' THEN 'Special' WHEN 'D' THEN 'Disassembly' END As "Type",
		A."PostDate",A."DueDate", A."ItemCode" As "ProductNo",C."ItemName" As "ProductName",A."Comments",
		A."StartDate",A."Priority",
		D."ItemCode",D."wareHouse",D."LineNum",D."BaseQty",
		D."PlannedQty",D."IssuedQty",
		D."BaseQty"-D."IssuedQty" As "AvaibleIssue", --D.*
		E."OnHand",E."IsCommited",E."OnOrder",F."OnHand"-F."IsCommited" As "StockAvaible"

	FROM "CAPTAINHOOK_PRD"."OWOR" A
		INNER JOIN "CAPTAINHOOK_PRD"."NNM1" B ON A."Series"=B."Series" 
		INNER JOIN "CAPTAINHOOK_PRD"."OITM" C ON A."ItemCode"=C."ItemCode"  -- JOIN TO FINAL ITEMCODE
		INNER JOIN "CAPTAINHOOK_PRD"."WOR1" D ON A."DocEntry"=D."DocEntry"
		INNER JOIN "CAPTAINHOOK_PRD"."OITM" E ON D."ItemCode"=E."ItemCode"  -- JOIN TO Material ItemCode
		INNER JOIN "CAPTAINHOOK_PRD"."OITW" F ON D."ItemCode"=F."ItemCode" AND D."wareHouse"=F."WhsCode"
	WHERE 
		A."Status"='R' AND A."DocEntry" IN(SELECT VAL FROM #TB_POEntry) AND D."IssueType"='M' AND
		D."BaseQty"-D."IssuedQty">0
	ORDER BY A."DocEntry", D."LineNum";
	
	DROP TABLE #TB_POEntry;
END;

 CALL CAPTAINHOOK_PRD."USP_ListAvaibleIssueLineOfProductionOrder"('10,14,15,16');

--SELECT * FROM "CAPTAINHOOK_PRD"."OITM" WHERE "ItemCode"='4GT0100'



