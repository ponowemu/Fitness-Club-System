package pl.smartica.trimfitmobile.viewModels

import android.content.Context
import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import pl.smartica.trimfitmobile.api.model.Event
import pl.smartica.trimfitmobile.repositories.CalendarRepository

class CalendarViewModel():ViewModel() {
    private var eventItems= MutableLiveData<MutableList<Event>>()
    private val repository= CalendarRepository()

    fun initialize(context: Context){
        eventItems = repository.getItems(context)
    }

    fun getEventList():LiveData<MutableList<Event>>{
        return eventItems
    }
}