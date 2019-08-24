package pl.smartica.trimfitmobile.api.model

/**
 * Data class that captures user information for logged in users retrieved from LoginRepository
 */
data class LoggedInUser(
    val userId: String,
/*    val userToken: String,
    val userType: Int,
    val userFirstName: String,
    val userLastName: String,*/
    val displayName: String
  //  val userPhotoUrl: String

)
/*user_id	integer($int32)
user_Token	string
user_Type	integer($int32)
user_Status	integer($int32)
user_Photo_Url	string
user_FirstName	string
user_LastName	string
user_login	string*/