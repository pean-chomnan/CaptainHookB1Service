ALTER PROCEDURE CAPTAINHOOK_PRD."USP_GetSO"(IN DocNum INT)
AS
BEGIN
	SELECT A."DocEntry",A."CardCode",A."CardName",
		C."Name" As "ContactName",A."NumAtCard",A."CurSource",
		A."Series",B."SeriesName",A."DocNum",A."DocDate",
		A."DocDueDate",
		A."TaxDate",
		A."DocTotalSy" As "TotalBFDiscount",
		A."DiscPrcnt",
		A."DiscSum",
		A."DocTotal",
		D."ItemCode",D."CodeBars",D."OpenQty" As "Quantity",
		D."Price",D."DiscPrcnt" As "LineDiscPrcnt",
		D."VatGroup",D."OpenSum" As "LineTotal",
		D."WhsCode",D."OcrCode",D."OcrCode2",
		D."UomCode",D."LineNum"
	
	FROM "CAPTAINHOOK_PRD"."ORDR" A 
		LEFT OUTER JOIN "CAPTAINHOOK_PRD"."NNM1" B ON A."Series"=B."Series"
		LEFT OUTER JOIN "CAPTAINHOOK_PRD"."OCPR" C ON A."CntctCode"=C."CntctCode"
		INNER JOIN "CAPTAINHOOK_PRD"."RDR1" D ON A."DocEntry"=D."DocEntry"
	WHERE A."DocStatus"='O' 
		AND A."CANCELED"='N'
		AND D."LineStatus"='O'
		AND A."DocNum"=DocNum;
END;

CALL CAPTAINHOOK_PRD."USP_GetSO"(212600001);

--SELECT * FROM "CAPTAINHOOK_PRD"."ORDR" 




