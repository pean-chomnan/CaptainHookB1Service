ALTER PROCEDURE CAPTAINHOOK_PRD."USP_LisOfGetPO"()
AS
BEGIN
	SELECT A."DocEntry",
		A."Series",B."SeriesName",A."DocNum",A."DocDate",
		A."DocDueDate",
		A."TaxDate",A."CardCode",A."CardName",
		C."Name" As "ContactName",A."NumAtCard",A."CurSource",
		A."DocTotalSy" As "TotalBFDiscount",
		A."DiscPrcnt",
		A."DiscSum",
		A."DocTotal"
	
	FROM "CAPTAINHOOK_PRD"."OPOR" A 
		LEFT OUTER JOIN "CAPTAINHOOK_PRD"."NNM1" B ON A."Series"=B."Series"
		LEFT OUTER JOIN "CAPTAINHOOK_PRD"."OCPR" C ON A."CntctCode"=C."CntctCode"
	WHERE A."DocStatus"='O' 
		AND A."CANCELED"='N';
END;

CALL CAPTAINHOOK_PRD."USP_LisOfGetPO"();
