-- drop function SplitString

ALTER function CAPTAINHOOK_PRD."SplitString" (TEXT nvarchar(5000),Str Varchar(20)) 
        returns Table (Num Integer,Val Nvarchar(5000))
language sqlscript   
as   
begin   
	  declare _items nvarchar(5000) ARRAY;
	  declare _text nvarchar(5000);
	  declare _index integer;
	  _text := :TEXT;
	  _index := 1;
	
	  WHILE LOCATE(:_text,:Str) > 0 DO
	  _items[:_index] := SUBSTR_BEFORE(:_text,:Str);
	  _text := SUBSTR_AFTER(:_text,:Str);
	  _index := :_index + 1;
	  END WHILE;
	  _items[:_index] := :_text;
	
	  rst = UNNEST(:_items) AS ("Val");
	  Return
	  SELECT ROW_NUMBER() OVER(ORDER BY "Val") As Num,"Val" As Val FROM :rst;
end;

  Select * From  CAPTAINHOOK_PRD."SplitString"('2021020478,2021020479,2021020480,2021020481,2021020482,2021020483,2021020484,2021020485,2021020486,2021020487',',');
 


