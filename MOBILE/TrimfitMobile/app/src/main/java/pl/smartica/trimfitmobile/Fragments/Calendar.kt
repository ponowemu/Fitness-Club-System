package pl.smartica.trimfitmobile.Fragments

import android.net.Uri
import android.os.Bundle
import android.util.Log
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ProgressBar
import androidx.lifecycle.ViewModelProviders
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import pl.smartica.trimfitmobile.R
import pl.smartica.trimfitmobile.data.adapters.ServiceAdapter
import pl.smartica.trimfitmobile.viewmodels.CalendarViewModel


class Calendar : Fragment() {

    private val TAG = "ProductsFragment"
    private var listener: Products.OnFragmentInteractionListener? = null
    lateinit var adapter: ServiceAdapter
    private lateinit var loadingBar: ProgressBar
    private lateinit var recyclerView: RecyclerView
    private lateinit var calendarViewModel: CalendarViewModel

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        val view = inflater.inflate(R.layout.fragment_calendar, container, false)
        recyclerView = view.findViewById<RecyclerView>(R.id.calendar_recycler)
        loadingBar = view.findViewById(R.id.loadingBar)
        setProgressBarVisible(true)
        calendarViewModel = ViewModelProviders.of(this).get(CalendarViewModel::class.java)
        calendarViewModel.initialize(this.context!!)

        // Inflate the layout for this fragment
        return view
    }

    override fun onResume() {
        super.onResume()
        Log.v(TAG,"Resumed")
    }


    private fun initRecyclerView() {
       /* adapter = ServiceAdapter(
            this.context!!,
            //mServicesViewModel.getServicesList().value!!
        )9*/
        recyclerView.adapter = adapter
        recyclerView.layoutManager = LinearLayoutManager(this.context)
    }

    private fun setProgressBarVisible(visible: Boolean) {
        if (visible)
            loadingBar.visibility = View.VISIBLE
        else
            loadingBar.visibility = View.GONE
    }


    // TODO: Rename method, update argument and hook method into UI event
    fun onButtonPressed(uri: Uri) {
        listener?.onFragmentInteraction(uri)

    }


}
