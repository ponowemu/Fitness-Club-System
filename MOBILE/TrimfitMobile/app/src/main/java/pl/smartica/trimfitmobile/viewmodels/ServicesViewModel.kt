package pl.smartica.trimfitmobile.viewmodels

import android.content.Context
import android.util.Log
import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import com.android.volley.Request
import com.android.volley.Response
import com.android.volley.toolbox.JsonArrayRequest
import org.json.JSONArray
import pl.smartica.trimfitmobile.Callback
import pl.smartica.trimfitmobile.model.Service
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