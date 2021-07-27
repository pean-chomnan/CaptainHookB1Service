 CALL "CAPTAINHOOK_PRD"._USP_GETMASTERDATA('OITM',0);

ALTER PROCEDURE "CAPTAINHOOK_PRD"._USP_GETMASTERDATA(in DTYPE NVARCHAR(10),in STATUS INT)
AS
	
BEGIN
	IF :DType='OITM' THEN	
		IF STATUS = 1 THEN
			SELECT TOP 50 OI."ItemCode",OI."ItemName",OI."ItmsGrpCod","UserText",OI."U_ItemCategory",(SELECT "Descr" FROM "NKI_ERP_PRD"."UFD1" WHERE "TableID"='OITM' AND "FldValue" = OI."U_ItemCategory") As ItemsCategory
				,OI."validFor" 
			FROM "NKI_ERP_PRD"."OITM" OI
			WHERE LEFT(OI."ItemCode",1) IN('A','B','D') AND  OI."ItemCode" NOT IN(SELECT "ENTRY" FROM "NKI_ERP_PRD"."_UST_MASTERDATA" WHERE "TYPE" IN(DType));
		ELSE 
			SELECT TOP 50 OI."ItemCode",OI."ItemName",OI."ItmsGrpCod","UserText",OI."U_ItemCategory",(SELECT "Descr" FROM "NKI_ERP_PRD"."UFD1" WHERE "TableID"='OITM' AND "FldValue"= OI."U_ItemCategory") As ItemsCategory
				,OI."validFor"
			FROM "NKI_ERP_PRD"."OITM" OI
			WHERE LEFT(OI."ItemCode",1) IN('A','B','D') AND OI."ItemCode" IN(SELECT "ENTRY" FROM "NKI_ERP_PRD"."_UST_MASTERDATA" WHERE "STATUS" IN(1) AND "TYPE" IN(DType));
		END IF;	
	ELSE IF :DType='OITB' THEN
		IF STATUS =1 THEN
			SELECT TOP 50 "ItmsGrpCod","ItmsGrpNam" FROM "NKI_ERP_PRD"."OITB" 
			WHERE "ItmsGrpCod" NOT IN(SELECT "ENTRY" FROM "NKI_ERP_PRD"."_UST_MASTERDATA" WHERE "TYPE" IN(DType));
		ElSE 
			SELECT TOP 50 "ItmsGrpCod","ItmsGrpNam" FROM "NKI_ERP_PRD"."OITB" 
			WHERE "ItmsGrpCod" IN(SELECT "ENTRY" FROM "NKI_ERP_PRD"."_UST_MASTERDATA" WHERE "STATUS" IN(1) AND "TYPE" IN(DType));
		END IF;
	ELSE IF :DType='OHEM' THEN
		IF STATUS =1 THEN
			SELECT TOP 50 OH."ExtEmpNo",'' as "Password",OH."jobTitle",OH."firstName",OH."lastName",OH."CostCenter",
				(SELECT SUBSTR_BEFORE("descriptio",'-') FROM "NKI_ERP_PRD"."OHPS" WHERE "posID"=OH."position") as positionID,
				(SELECT LEFT("Remarks",2) FROM "NKI_ERP_PRD"."OUDP" WHERE "Code" IN(OH."dept")) as dept,
				(SELECT "Remarks" FROM "NKI_ERP_PRD"."OUBR" B WHERE "Code" IN(OH."branch")) as branch,OH."email",OH."Active" FROM "NKI_ERP_PRD"."OHEM" OH 
			WHERE OH."ExtEmpNo" NOT IN(SELECT "ENTRY" FROM "NKI_ERP_PRD"."_UST_MASTERDATA" WHERE "TYPE" IN(DType));
		ELSE
			SELECT TOP 50 OH."ExtEmpNo",'' as "Password",OH."jobTitle",OH."firstName",OH."lastName",OH."CostCenter",
				(SELECT SUBSTR_BEFORE("descriptio",'-') FROM "NKI_ERP_PRD"."OHPS" WHERE "posID"=OH."position") as positionID,
				(SELECT LEFT("Remarks",2) FROM "NKI_ERP_PRD"."OUDP" WHERE "Code" IN(OH."dept")) as dept,
				(SELECT "Remarks" FROM "NKI_ERP_PRD"."OUBR" B WHERE "Code" IN(OH."branch")) as branch,OH."email",OH."Active" FROM "NKI_ERP_PRD"."OHEM" OH 
			WHERE OH."ExtEmpNo" IN(SELECT "ENTRY" FROM "NKI_ERP_PRD"."_UST_MASTERDATA" WHERE "STATUS" IN(1) AND "TYPE" IN(DType));
		END IF;
	ELSE IF :DType='OHPS' THEN
		IF STATUS =1 THEN
			SELECT TOP 50 SUBSTR_BEFORE("descriptio",'-') as posID,"name","descriptio" FROM "NKI_ERP_PRD"."OHPS" 
			WHERE SUBSTR_BEFORE("descriptio",'-') NOT IN(SELECT "ENTRY" FROM "NKI_ERP_PRD"."_UST_MASTERDATA" WHERE "TYPE" IN(DType));
		ELSE
			SELECT TOP 50 SUBSTR_BEFORE("descriptio",'-') as posID,"name","descriptio" FROM "NKI_ERP_PRD"."OHPS" 
			WHERE SUBSTR_BEFORE("descriptio",'-') IN(SELECT "ENTRY" FROM "NKI_ERP_PRD"."_UST_MASTERDATA" WHERE "STATUS" IN(1) AND "TYPE" IN(DType));
		END IF;
	ELSE IF :DType='OUDP' THEN
		IF STATUS =1 THEN
			SELECT TOP 100 LEFT("Remarks",2) As Code,"Name","Remarks" FROM "NKI_ERP_PRD"."OUDP" 
			WHERE "Code" > 0 AND LEFT("Remarks",2) NOT IN(SELECT "ENTRY" FROM "NKI_ERP_PRD"."_UST_MASTERDATA" WHERE "TYPE" IN(DType));
		ELSE
			SELECT TOP 50 LEFT("Remarks",2) As Code,"Name","Remarks" FROM "NKI_ERP_PRD"."OUDP" 
			WHERE "Code" > 0 AND LEFT("Remarks",2) IN(SELECT "ENTRY" FROM "NKI_ERP_PRD"."_UST_MASTERDATA" WHERE "STATUS" IN(1) AND "TYPE" IN(DType));
		END IF;
	ELSE IF :DType='OUBR' THEN
		IF STATUS =1 THEN
			SELECT TOP 50 "Code","Name","Remarks" FROM "NKI_ERP_PRD"."OUBR" 
			WHERE "Code" > 0 AND "Remarks" NOT IN(SELECT "ENTRY" FROM "NKI_ERP_PRD"."_UST_MASTERDATA" WHERE "TYPE" IN(DType));
		ELSE
			SELECT TOP 50 "Code","Name","Remarks" FROM "NKI_ERP_PRD"."OUBR" 
			WHERE "Code" > 0 AND "Remarks" IN(SELECT "ENTRY" FROM "NKI_ERP_PRD"."_UST_MASTERDATA" WHERE "STATUS" IN(1) AND "TYPE" IN(DType));
		END IF;
	ELSE IF :DType='OPRC' THEN
		IF STATUS =1 THEN
			SELECT TOP 50 "PrcCode","PrcName","DimCode","CCTypeCode" FROM "NKI_ERP_PRD"."OPRC" 
			WHERE "Locked"= 'N' AND "DimCode"='3' AND "PrcCode" NOT IN(SELECT "ENTRY" FROM "NKI_ERP_PRD"."_UST_MASTERDATA" WHERE "TYPE" IN(DType));
		ELSE
			SELECT TOP 50 "PrcCode","PrcName","DimCode","CCTypeCode" FROM "NKI_ERP_PRD"."OPRC" 
			WHERE "Locked"= 'N' AND "DimCode"='3' AND "PrcCode" IN(SELECT "ENTRY" FROM "NKI_ERP_PRD"."_UST_MASTERDATA" WHERE "STATUS" IN(1) AND "TYPE" IN(DType));
		END IF;
	ELSE IF :DType='OCRD1' THEN
		IF STATUS =1 THEN
		
			SELECT TOP 50 C."CardCode",C."CardName",A."BRANCH"
				,A."BRANCH" || ' ' || A."STREETNO" || ' ' || A."MOONO" || ' ' || A."BUILDINGNO" ||' ' || A."BUILDING" 
				|| ' ' || A."STREET" || ' ' || A."FLOORNO" || ' ' || A."BLOCK" || ' ' || A."CITY" || ' '
				|| A."COUNTY" || ' ' || A."ZIPCODE" as Address
				,C."LicTradNum"
			FROM(
			
				SELECT C1."CardCode"
				,IFNULL(C1."Address",'') as BRANCH
				,IFNULL(C1."StreetNo",'') as StreetNo
				,IFNULL(C1."U_MooNo",'') as MooNo
				,IFNULL(C1."U_BuildingNo",'') as BuildingNo
				,IFNULL(C1."Building",'') as Building
				,IFNULL(C1."Street",'') as Street
				,IFNULL(C1."U_FloorNo",'') as FloorNo
				,IFNULL(C1."Block",'') as Block
				,IFNULL(C1."City",'') as City
				,IFNULL(C1."County",'') as County
				,IFNULL(C1."ZipCode",'') as ZipCode
				FROM "NKI_ERP_PRD"."CRD1" C1 
				--RIGHT JOIN "NKI_ERP_PRD"."CRD1" C1 ON C."CardCode"=C1."CardCode"
				WHERE C1."AdresType" IN('B')
				
			)as A RIGHT JOIN "NKI_ERP_PRD"."OCRD" C on A."CardCode" = C."CardCode" 
			WHERE C."CardType" IN('C') AND LEFT(C."CardCode",4) IN('CL01','CL02','CL03') AND C."CardCode" NOT IN('CL01-99999','CL02-99999') 
			AND C."CardCode" NOT IN(SELECT "ENTRY" FROM "NKI_ERP_PRD"."_UST_MASTERDATA" WHERE "TYPE" IN(DType));
			
		ELSE
		
			SELECT TOP 50 C."CardCode",C."CardName",A."BRANCH"
				,A."BRANCH" || ' ' || A."STREETNO" || ' ' || A."MOONO" || ' ' || A."BUILDINGNO" ||' ' || A."BUILDING" 
				|| ' ' || A."STREET" || ' ' || A."FLOORNO" || ' ' || A."BLOCK" || ' ' || A."CITY" || ' '
				|| A."COUNTY" || ' ' || A."ZIPCODE" as Address
				,C."LicTradNum"
			FROM(
			
				SELECT C1."CardCode"
				,IFNULL(C1."Address",'') as BRANCH
				,IFNULL(C1."StreetNo",'') as StreetNo
				,IFNULL(C1."U_MooNo",'') as MooNo
				,IFNULL(C1."U_BuildingNo",'') as BuildingNo
				,IFNULL(C1."Building",'') as Building
				,IFNULL(C1."Street",'') as Street
				,IFNULL(C1."U_FloorNo",'') as FloorNo
				,IFNULL(C1."Block",'') as Block
				,IFNULL(C1."City",'') as City
				,IFNULL(C1."County",'') as County
				,IFNULL(C1."ZipCode",'') as ZipCode
				FROM "NKI_ERP_PRD"."CRD1" C1 
				--RIGHT JOIN "NKI_ERP_PRD"."CRD1" C1 ON C."CardCode"=C1."CardCode"
				WHERE C1."AdresType" IN('B')
				
			)as A RIGHT JOIN "NKI_ERP_PRD"."OCRD" C on A."CardCode" = C."CardCode" 
			WHERE C."CardType" IN('C') AND LEFT(C."CardCode",4) IN('CL01','CL02','CL03') AND C."CardCode" NOT IN('CL01-99999','CL02-99999') 
			AND C."CardCode" IN(SELECT "ENTRY" FROM "NKI_ERP_PRD"."_UST_MASTERDATA" WHERE "STATUS" IN(1) AND "TYPE" IN(DType));
		END IF;
	ELSE IF :DType='OCRD2' THEN
		IF STATUS =1 THEN
		
			SELECT TOP 50 C."CardCode",C."CardName",A."BRANCH"
				,A."BRANCH" || ' ' || A."STREETNO" || ' ' || A."MOONO" || ' ' || A."BUILDINGNO" ||' ' || A."BUILDING" 
				|| ' ' || A."STREET" || ' ' || A."FLOORNO" || ' ' || A."BLOCK" || ' ' || A."CITY" || ' '
				|| A."COUNTY" || ' ' || A."ZIPCODE" as Address
				,C."LicTradNum"
			FROM(
			
				SELECT C1."CardCode"
				,IFNULL(C1."Address",'') as BRANCH
				,IFNULL(C1."StreetNo",'') as StreetNo
				,IFNULL(C1."U_MooNo",'') as MooNo
				,IFNULL(C1."U_BuildingNo",'') as BuildingNo
				,IFNULL(C1."Building",'') as Building
				,IFNULL(C1."Street",'') as Street
				,IFNULL(C1."U_FloorNo",'') as FloorNo
				,IFNULL(C1."Block",'') as Block
				,IFNULL(C1."City",'') as City
				,IFNULL(C1."County",'') as County
				,IFNULL(C1."ZipCode",'') as ZipCode
				FROM "NKI_ERP_PRD"."CRD1" C1 
				--RIGHT JOIN "NKI_ERP_PRD"."CRD1" C1 ON C."CardCode"=C1."CardCode"
				WHERE C1."AdresType" IN('B')
				
			)as A RIGHT JOIN "NKI_ERP_PRD"."OCRD" C on A."CardCode" = C."CardCode" 
			WHERE C."CardType" IN('S') AND C."CardCode" NOT IN('VL01-99999','VL02-99999','VL03-99999','VL04-99999','VL05-99999','VL06-99999','VL07-99999','VL08-99999','VL09-99999')  
			AND C."CardCode" NOT IN(SELECT "ENTRY" FROM "NKI_ERP_PRD"."_UST_MASTERDATA" WHERE "TYPE" IN(DType));
		ELSE
			SELECT TOP 50 C."CardCode",C."CardName",A."BRANCH"
				,A."BRANCH" || ' ' || A."STREETNO" || ' ' || A."MOONO" || ' ' || A."BUILDINGNO" ||' ' || A."BUILDING" 
				|| ' ' || A."STREET" || ' ' || A."FLOORNO" || ' ' || A."BLOCK" || ' ' || A."CITY" || ' '
				|| A."COUNTY" || ' ' || A."ZIPCODE" as Address
				,C."LicTradNum"
			FROM(
			
				SELECT C1."CardCode"
				,IFNULL(C1."Address",'') as BRANCH
				,IFNULL(C1."StreetNo",'') as StreetNo
				,IFNULL(C1."U_MooNo",'') as MooNo
				,IFNULL(C1."U_BuildingNo",'') as BuildingNo
				,IFNULL(C1."Building",'') as Building
				,IFNULL(C1."Street",'') as Street
				,IFNULL(C1."U_FloorNo",'') as FloorNo
				,IFNULL(C1."Block",'') as Block
				,IFNULL(C1."City",'') as City
				,IFNULL(C1."County",'') as County
				,IFNULL(C1."ZipCode",'') as ZipCode
				FROM "NKI_ERP_PRD"."CRD1" C1 
				--RIGHT JOIN "NKI_ERP_PRD"."CRD1" C1 ON C."CardCode"=C1."CardCode"
				WHERE C1."AdresType" IN('B')
				
			)as A RIGHT JOIN "NKI_ERP_PRD"."OCRD" C on A."CardCode" = C."CardCode" 
			WHERE C."CardType" IN('S') AND C."CardCode" NOT IN('VL01-99999','VL02-99999','VL03-99999','VL04-99999','VL05-99999','VL06-99999','VL07-99999','VL08-99999','VL09-99999') 
			AND C."CardCode" IN(SELECT "ENTRY" FROM "NKI_ERP_PRD"."_UST_MASTERDATA" WHERE "STATUS" IN(1) AND "TYPE" IN(DType));
		END IF;
	ELSE
	
	END IF;
	END IF;
	END IF;
	END IF;
	END IF;
	END IF;
	END IF;
	END IF;
	END IF;
END;
