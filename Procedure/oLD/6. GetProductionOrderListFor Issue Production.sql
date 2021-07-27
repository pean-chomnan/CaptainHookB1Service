--DROP PROCEDURE CAPTAINHOOK_PRD."USP_ListProductionOrder";

CREATE PROCEDURE CAPTAINHOOK_PRD."USP_ListOfProductionOrderForIssueProduction"()
AS
BEGIN
	SELECT 
		A."DocEntry",A."DocNum",A."Series",B."SeriesName",
		CASE A."Type" WHEN 'S' THEN 'Standard' WHEN 'P' THEN 'Special' WHEN 'D' THEN 'Disassembly' END As "Type",
		"PostDate","DueDate", A."ItemCode" As "ProductNo",
		C."ItemName" As "ProductName","Comments",
		"StartDate","Priority"
	FROM "CAPTAINHOOK_PRD"."OWOR" A
		INNER JOIN "CAPTAINHOOK_PRD"."NNM1" B ON A."Series"=B."Series" 
		INNER JOIN "CAPTAINHOOK_PRD"."OITM" C ON A."ItemCode"=C."ItemCode"
		INNER JOIN
		(
			SELECT DISTINCT "DocEntry" FROM "CAPTAINHOOK_PRD"."WOR1" WHERE  "IssueType"='M'
		) D ON A."DocEntry"=D."DocEntry"
		
	WHERE "Status"='R' -- AND A."PlannedQty">A."CmpltQty"
	ORDER BY A."DocEntry";
End;

CALL CAPTAINHOOK_PRD."USP_ListOfProductionOrderForIssueProduction"();
	
	
