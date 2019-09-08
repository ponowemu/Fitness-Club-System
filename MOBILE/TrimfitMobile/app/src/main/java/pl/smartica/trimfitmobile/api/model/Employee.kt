package pl.smartica.trimfitmobile.api.model

import org.json.JSONObject

class Employee {
    var employeeId: Int? = null
    var employeeFirstName : String? = null
    var employeeLastName : String? = null

    constructor(item: JSONObject){
        employeeId = item.getInt("employee_Id")
        employeeFirstName = item.getString("employee_Firstname")
        employeeLastName = item.getString("employee_Lastname")
    }
}


/*    "employee": {
      "employee_Id": 0,
      "employee_Firstname": "string",
      "employee_Lastname": "string",
      "employee_Birthdate": "2019-09-08T10:35:44.336Z",
      "employee_Gender": 0,
      "employee_Added": "2019-09-08T10:35:44.336Z",
      "position_Id": [
        0
      ],*/