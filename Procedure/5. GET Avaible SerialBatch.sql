ALTER PROCEDURE CAPTAINHOOK_PRD."USP_GetAvaibleSerialBatch"(IN ItemCode Nvarchar(50),IN WhsCode Varchar(30))
AS
BEGIN
	SELECT 
		T0."ItemCode" AS "ItemCode", T1."DistNumber" AS "DistNumber", 
		T1."MnfSerial" AS "BatchAttribute1", T0."Quantity" AS "AvailableQty", 
		T1."ExpDate" AS "ExpiryDate", T0."CommitQty" AS "AllocatedQty", 
		T0."CountQty" AS "CountQty", T0."SysNumber", 
		T2."BinCode",T1."U_ACT_WeightOnBatch",
		CAST(T1."U_CompanyAddress" AS Nvarchar(254)) As "U_CompanyAddress",
		T1."U_BarCodeBoxNumber",T1."U_SmokingSystem",'Batch' As "Status"
	FROM CAPTAINHOOK_PRD."OBTQ" T0 
		INNER JOIN CAPTAINHOOK_PRD."OBTN" T1 ON T1."ItemCode" = T0."ItemCode" AND T1."SysNumber" = T0."SysNumber" 
		LEFT OUTER JOIN 
		(
			SELECT 
				T0."SnBMDAbs", T0."BinAbs", T1."BinCode" 
			FROM CAPTAINHOOK_PRD."OBBQ" T0 
				INNER JOIN CAPTAINHOOK_PRD."OBIN" T1 ON T1."AbsEntry" = T0."BinAbs" 
			WHERE T0."ItemCode"=ItemCode AND T0."OnHandQty" > (0) AND T0."WhsCode" = WhsCode
		) AS T2 ON T0."MdAbsEntry" = T2."SnBMDAbs" 
	WHERE T0."ItemCode" = ItemCode AND T0."WhsCode" = WhsCode AND T1."Status" <= (0) AND T0."Quantity" <> (0)
	
	UNION 
	
	SELECT 
		T0."ItemCode" AS "ItemCode", T1."DistNumber" AS "DistNumber", 
		T1."MnfSerial" AS "BatchAttribute1", T0."Quantity" AS "AvailableQty", 
		T1."ExpDate" AS "ExpiryDate", T0."CommitQty" AS "AllocatedQty", 
		T0."CountQty" AS "CountQty", T0."SysNumber", 
		T2."BinCode",T1."U_ACT_WeightOnBatch",
		CAST(T1."U_CompanyAddress" AS Nvarchar(254)) As "U_CompanyAddress",
		T1."U_BarCodeBoxNumber",T1."U_SmokingSystem",'Serial' As "Status"
	FROM CAPTAINHOOK_PRD."OSRQ" T0 
		INNER JOIN CAPTAINHOOK_PRD."OSRN" T1 ON T1."ItemCode" = T0."ItemCode" AND T1."SysNumber" = T0."SysNumber" 
		LEFT OUTER JOIN 
		(
			SELECT 
				T0."SnBMDAbs", T0."BinAbs", T1."BinCode" 
			FROM CAPTAINHOOK_PRD."OBBQ" T0 
				INNER JOIN CAPTAINHOOK_PRD."OBIN" T1 ON T1."AbsEntry" = T0."BinAbs" 
			WHERE T0."ItemCode"=ItemCode AND T0."OnHandQty" > (0) AND T0."WhsCode" = WhsCode
		) AS T2 ON T0."MdAbsEntry" = T2."SnBMDAbs" 
	WHERE T0."ItemCode" = ItemCode AND T0."WhsCode" = WhsCode  AND T0."Quantity" <> (0);
END;

 CALL CAPTAINHOOK_PRD."USP_GetAvaibleSerialBatch"('1IGBL0008','03.HKT05');
 
 
 
 
 
