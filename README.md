
# DylamicLinq
A light library to generate lambada expression through object or JSON
## Samples
**Step1.** Greate instance of your object. <br/>
`var obj = new Student();` // here 'Student' could replaced by any of your own object <br/>
<br/>
**Step2.** Initiallize IDLinqTool: <br/>
`IDLinqTool tool = new DLinqTool();`<br/><br/>

**Step3.** Generate lambda expression:<br/>
1. *Group By* expressoin: <br/>
`var lambda = tool.GetGroupByLambda(obj, your-group-by-field);` //i => Convert(i.your-group-by-field)<br/><br/>
2. *Order By* expression:<br/>
`var orderLambda = tool.GetOrderByLambda(obj, your-order-by-field);` //i => Convert(i.your-order-by-field)<br/> <br/>3. *Where*<br/>
 `var stdLambda = tool.GetLambda(obj, searchList);`//i => ((bool-expression-1) AndAlso (bool-expression-2))<br/><br/>
4. *Compile* <br/>
  `above-lambda-result.compile();` //use compile method to produce lambda expression
  <br/></br>
**Note:**  _You could find the details in /Main/Main/Program.cs_
<br/></br>

## SearchFlag
 * **Type**: _enum_ <br/>
 * **Value:**   
   * _AND_
   * _OR_

## Filter Class
* **Type**: _Class_
* **Property**:
    * filed: filedName [_string_]
    * data: filedValue [_string_]
    * op: operator ption [_string_]

## SearchFilter Class
* **Type**: _Class_

 It's collection of **Filter** class

## Search opration option
* **Type**: _string_
* **Options**:
   * "eq" -> equal
   * "ne" -> not equal
   * "cn" -> contains   * "nc" -> not contains
   * "lt" -> less
   * "le" -> less or equal
   * "gt" -> greater than
   * "ge" -> greater or equal

## IDLinqTool interface
  * **GetLambda<T>(T obj, SearchFilter filter)** *Return expression*<br/>
      
       * _**Parameters:**_
            * **obj**: source object
            * **filter**: filter rules
      
       *  _**Description:**_
            * **Summary:** Get simple lambda expression incluing verbs ***'AndAlso'*** or ***'Or'***
        * **filter:** Instance of object ***SearchFilter*** list of 
        
        { filed: filedName,
          op: operation[eq,nq,cn etc..],
          data: data
        }
    
  * **GetLambda<T>(T obj, string jsonFilter)** *Return expression*
      
    *   _**Parameters:**_
          * **obj**: _source object_
          * **jsonFilter**: _**SearchFilter's** serializition string_ 
       
    *  _**Description:**_
          * **Summary:** Get simple lambda expression incluing verbs ***'AndAlso'*** or ***'Or'***
   
* **GetLambda<T>(T obj, List<SearchFilter> searchFilters, SearchFlag searchFlag = SearchFlag.AND)** *Return expression*
  
  *   _**Parameters:**_
        * **obj**: source object
        * **searchFilters**: ***SearchFilter*** list 
        * **SearchFlag:** An enum inluding ***'AND'*** and * **Or***
        
        
  *   _**Description:**_
        * **Summary:** Get simple lambda expression incluing verbs ***'AndAlso'*** or ***'Or'***
    
*   **GetOrderByLambda<T>(T obj, string properName)** *Return expression* 
      
  *  _**Parameters:**_
        * **obj**: _source object_
        * **properName**: property need to include in lambda expression
      
  *  _**Description:**_
        * **Summary:** Get simple lambda expression incluing verbs ***'AndAlso'*** or ***'Or'***
      
*   **GetGroupByLambda<T>(T obj, string properName)** *Return expression*
      
    * _**Parameters:**_
        * **obj**: _source object_
        * **properName**: property need to include in lambda expression

    * _**Description:**_
        * **Summary:** Get simple lambda expression incluing verbs ***'AndAlso'*** or ***'Or'***
