package pl.smartica.trimfitmobile.api.model

import org.json.JSONObject

class User {
    var userId: Int = 0
    lateinit var userToken: String
    var userType: Int = 0
    lateinit var userFirstName: String
    lateinit var userLastName: String
    lateinit var userPhotoUrl: String

    constructor(item: JSONObject) {
        userId = item.getString("user_id").toInt()
        userToken = item.getString("user_token")
        userFirstName = item.getString("user_FirstName")
        userLastName = item.getString("user_LastName")
        userPhotoUrl = item.getString("user_Photo_Url")
        userType = item.getString("service_Duration").toInt()
    }

}

