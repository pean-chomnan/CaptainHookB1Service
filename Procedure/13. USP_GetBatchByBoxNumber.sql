-- DROP PROCEDURE CAPTAINHOOK_PRD."USP_GetBatchByBoxNumber"
Alter PROCEDURE CAPTAINHOOK_PRD."USP_GetBatchByBoxNumber"(IN WhsCode Varchar(30),IN ItemCode Nvarchar(50),IN BoxNumber Varchar(100))
AS
BEGIN	
	SELECT DISTINCT
		T0."ItemCode", T1."DistNumber" AS "DistNumber", 
		T1."MnfSerial" AS "BatchAttribute1", T0."Quantity" AS "AvailableQty", 
		T1."ExpDate" AS "ExpiryDate", T0."CommitQty" AS "AllocatedQty", 
		T0."CountQty" AS "CountQty", T0."SysNumber", 
		T2."BinCode",T1."U_ACT_WeightOnBatch",
		CAST(T1."U_CompanyAddress" AS Nvarchar(254)) As "U_CompanyAddress",
		T1."U_BarCodeBoxNumber",T1."U_SmokingSystem"
	FROM CAPTAINHOOK_PRD."OBTQ" T0 
		INNER JOIN CAPTAINHOOK_PRD."OBTN" T1 ON T1."ItemCode" = T0."ItemCode" AND T1."SysNumber" = T0."SysNumber" 
		LEFT OUTER JOIN 
		(
			SELECT 
				T0."SnBMDAbs", T0."BinAbs", T1."BinCode" 
			FROM CAPTAINHOOK_PRD."OBBQ" T0 
				INNER JOIN CAPTAINHOOK_PRD."OBIN" T1 ON T1."AbsEntry" = T0."BinAbs" 
			WHERE T0."ItemCode"=ItemCode 
				AND T0."WhsCode" = WhsCode
		) AS T2 ON T0."MdAbsEntry" = T2."SnBMDAbs" 
	WHERE T0."ItemCode" = ItemCode AND T0."WhsCode" = WhsCode 
		AND T1."Status" <= (0)
		AND IFNULL(T1."U_BarCodeBoxNumber",'')=BoxNumber;
END;

  CALL CAPTAINHOOK_PRD."USP_GetBatchByBoxNumber"('03.HKT05','1IGSTIOD','1');


 
