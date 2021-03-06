package pl.smartica.trimfitmobile.viewModels

import android.content.Context
import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import android.util.Patterns
import pl.smartica.trimfitmobile.data.LoginRepository
import pl.smartica.trimfitmobile.data.Result

import pl.smartica.trimfitmobile.R
import pl.smartica.trimfitmobile.data.model.LoggedInUserView
import pl.smartica.trimfitmobile.data.model.LoginFormState
import pl.smartica.trimfitmobile.data.model.LoginResult

class LoginViewModel(private val loginRepository: LoginRepository) : ViewModel() {

    private val _loginForm = MutableLiveData<LoginFormState>()
    val loginFormState: LiveData<LoginFormState> = _loginForm

    private val _loginResult = MutableLiveData<LoginResult>()
    val loginResult: LiveData<LoginResult> = _loginResult

    suspend fun login(username: String, password: String, context: Context) {
        // can be launched in a separate asynchronous job
        val result = loginRepository.login(username, password ,context)

        if (result is Result.Success) {
            _loginResult.value = LoginResult(
                success = LoggedInUserView(displayName = result.data.displayName)
            )
        } else {
            _loginResult.value =
                LoginResult(error = R.string.login_failed)
        }
    }

    fun loginDataChanged(username: String, password: String) {
        if (!isPasswordValid(password)) {
            _loginForm.value =
                LoginFormState(passwordError = R.string.invalid_password)
        } else {
            _loginForm.value =
                LoginFormState(isDataValid = true)
        }
    }

    // A placeholder username validation check
    private fun isUserNameValid(username: String): Boolean {
        return if (username.contains('@')) {
            Patterns.EMAIL_ADDRESS.matcher(username).matches()
        } else {
            username.isNotBlank()
        }
    }

    // A placeholder password validation check
    private fun isPasswordValid(password: String): Boolean {
        return password.length > 3;
    }
}
