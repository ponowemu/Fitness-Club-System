package pl.smartica.trimfitmobile.viewModels

import android.content.Context
import androidx.lifecycle.ViewModel
import pl.smartica.trimfitmobile.repositories.CalendarRepository

class CalendarViewModel():ViewModel() {

    private val repository:CalendarRepository = CalendarRepository()

    fun initialize(context: Context){

    }
}