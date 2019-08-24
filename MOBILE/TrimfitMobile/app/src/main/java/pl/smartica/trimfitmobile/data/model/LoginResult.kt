package pl.smartica.trimfitmobile.data.model

import pl.smartica.trimfitmobile.data.model.LoggedInUserView

/**
 * Authentication result : success (user details) or error message.
 */
data class LoginResult(
    val success: LoggedInUserView? = null,
    val error: Int? = null
)
