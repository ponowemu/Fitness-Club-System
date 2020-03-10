package pl.smartica.trimfitmobile.api.model

import org.json.JSONObject

class Activity {
    var activityId: Int? = null
    var activityName: String? = null
    var activityDescription:String? = null

    constructor(item: JSONObject) {
        activityId = item.getString("activity_Id").toInt()
        activityName = item.getString("activity_Name")
        activityDescription = item.getString("activity_Description")
    }

}