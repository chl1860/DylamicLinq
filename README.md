# DylamicLinq
A light library to generate lambada expression through object or JSON

### Samples
**Step1.** Greate instance of your object. <br/>
`var obj = new Student();` // here 'Student' could replaced by any of your own object <br/>
<br/>
**Step2.** Initiallize IDLinqTool: <br/>
`IDLinqTool tool = new DLinqTool();`<br/><br/>
**Step3.** Generate lambda expression:<br/>
1. *Group By* expressoin: <br/>
`var lambda = tool.GetGroupByLambda(obj, your-group-by-field);` //i => Convert(i.your-group-by-field)<br/><br/>
2. *Order By* expression:<br/>
`var orderLambda = tool.GetOrderByLambda(obj, your-order-by-field);` //i => Convert(i.your-order-by-field)<br/> <br/>
3. *Where*<br/>
 `var stdLambda = tool.GetLambda(obj, searchList);`//i => ((bool-expression-1) AndAlso (bool-expression-2))<br/><br/>
4. *Compile* <br/>
  `above-ambda-result.compile();` //use compile method to produce lambda expression
  <br/></br>
**Note:**  _You could find the details in /Main/Main/Program.cs_

### SearchFilter Class
  <ul>
    <li>***Type:*** Class</li>
    <li>
      Property:
      <ul>
        <li>filed: filedName _string_</li>
        <li>data: filedValue _string_</li>
        <li>op: oprator ption _string_</li>
      </ul>
    </li>
  </ul>
<br/><br/>

### Search opration option
 "eq" -> equal<br/>
<br/><br/>

### IDLinqTool interface
  <ul>
    <li>GetLambda<T>(T obj, SearchFilter filter) *Return expression*<br/>
      <ul>
        _**Parameters:**_
        <li>**obj**: source object</li>
        <li>**filter**: filter rules</li>
      </ul>
      <ul>
        _**Description:**_
        <li>**Summary:** Get simple lambda expression incluing verbs ***'AndAlso'*** or ***'Or'***</li>
        <li>**filter:** Instance of object ***SearchFilter*** {filed: filedName, op: operation[eq,nq,cn etc..], data: data}</li>
      </ul>
    </li>
    <br/>
    <li>GetLambda<T>(T obj, string jsonFilter) *Return expression* <br/>
      <ul>
          _**Parameters:**_
          <li>**obj**: source object</li>
          <li>**jsonFilter**: ***SearchFilter***'s serializition string </li>
        </ul>
        <ul>
          _**Description:**_
          <li>**Summary:** Get simple lambda expression incluing verbs ***'AndAlso'*** or ***'Or'***</li>
      </ul>
    </li><br/>
    <li>
      GetLambda<T>(T obj, List<SearchFilter> searchFilters, SearchFlag searchFlag = SearchFlag.AND) *Return expression* <br/>
      <ul>
          _**Parameters:**_
          <li>**obj**: source object</li>
          <li>**searchFilters**: ***SearchFilter*** list </li>
          <li>**SearchFlag:** An enum inluding ***'AND'*** and ***Or***</li>
        </ul>
        <ul>
          _**Description:**_
          <li>**Summary:** Get simple lambda expression incluing verbs ***'AndAlso'*** or ***'Or'***</li>
      </ul>
    </li>
    <br/>
    <li>GetOrderByLambda<T>(T obj, string properName) *Return expression* <br/>
      <ul>
        _**Parameters:**_
        <li>**obj**: source object</li>
        <li>**properName**: property need to include in lambda expression</li>
      </ul>
      <ul>
        _**Description:**_
        <li>**Summary:** Get simple lambda expression incluing verbs ***'AndAlso'*** or ***'Or'***</li>
      </ul>
    </li>
    <br/>
    <li>
      GetGroupByLambda<T>(T obj, string properName) *Return expression* <br/>
      <ul>
          _**Parameters:**_
          <li>**obj**: source object</li>
          <li>**properName**: property need to include in lambda expression</li>
        </ul>
        <ul>
          _**Description:**_
          <li>**Summary:** Get simple lambda expression incluing verbs ***'AndAlso'*** or ***'Or'***</li>
        </ul>
    </li>
  </ul>
