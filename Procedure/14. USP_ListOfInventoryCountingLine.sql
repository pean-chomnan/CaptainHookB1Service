--	drop procedure USP_ListOfInventoryCoutingLine

ALTER PROCEDURE CAPTAINHOOK_PRD."USP_ListOfInventoryCoutingLine"(IN CountingEntry Varchar(254))
AS
BEGIN
	
	_Entry=(SELECT * FROM CAPTAINHOOK_PRD."SplitString"(:CountingEntry,','));
	
	CREATE LOCAL TEMPORARY COLUMN TABLE #TB_ENTRY AS(Select * from :_Entry);

--	SELECT * FROM #TB_ENTRY;

	SELECT 
		T0."DocEntry", T0."LineNum", T0."ItemCode", T0."ItemDesc", 
		T0."Freeze", T0."WhsCode", T0."InWhsQty", 
		T0."Counted", T0."CountQty", T0."Remark", T0."BarCode", 
		T0."InvUoM", T0."Difference", 
		T0."DiffPercen", T0."CountDate", T0."CountTime", 
		T0."ProjCode", T0."OcrCode", 
		T0."LineStatus", T0."BinEntry", T0."VisOrder", 
		T0."OcrCode2", T0."OcrCode3", 
		T0."OcrCode4", T0."OcrCode5", T0."FirmCode", 
		T0."SuppCatNum", T0."PrefVendor", 
		T0."CountDiff", T0."CountDiffP", 
		T0."UomCode", T0."UomQty" 
	FROM CAPTAINHOOK_PRD."INC1" T0 
		WHERE T0."DocEntry" IN(SELECT VAL FROM #TB_ENTRY) AND T0."Counted" = ('Y') AND T0."LineStatus" = ('O') 
	ORDER BY T0."DocEntry", T0."VisOrder";
	
	DROP TABLE #TB_ENTRY;
END;

		CALL CAPTAINHOOK_PRD."USP_ListOfInventoryCoutingLine"('7,15,21,37');


