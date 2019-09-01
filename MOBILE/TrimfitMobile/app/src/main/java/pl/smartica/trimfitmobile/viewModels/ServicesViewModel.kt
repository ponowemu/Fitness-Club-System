package pl.smartica.trimfitmobile.viewModels

import android.content.Context
import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import pl.smartica.trimfitmobile.api.model.Service
import pl.smartica.trimfitmobile.repositories.ServicesRepository

class ServicesViewModel : ViewModel(){
    private var mServicesList = MutableLiveData<MutableList<Service>>()
    private var mServicesRepository: ServicesRepository = ServicesRepository()

    fun initialize(context: Context){
        mServicesList = mServicesRepository.getServiceList(context);
    }

    fun getServicesList(): LiveData<MutableList<Service>>{
        return mServicesList;
    }

}