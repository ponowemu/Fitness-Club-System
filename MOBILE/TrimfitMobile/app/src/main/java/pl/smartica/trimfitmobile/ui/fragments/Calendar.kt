package pl.smartica.trimfitmobile.ui.fragments

import android.net.Uri
import android.os.Bundle
import android.util.Log
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ProgressBar
import androidx.lifecycle.Observer
import androidx.lifecycle.ViewModelProviders
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import pl.smartica.trimfitmobile.R
import pl.smartica.trimfitmobile.adapters.CalendarAdapter
import pl.smartica.trimfitmobile.adapters.ServiceAdapter
import pl.smartica.trimfitmobile.viewModels.CalendarViewModel


class Calendar : Fragment() {

    private val TAG = "ProductsFragment"
    private var listener: Products.OnFragmentInteractionListener? = null
    lateinit var adapter: CalendarAdapter
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
        loadingBar = view.findViewById(R.id.loadingBarCalendar)
        setProgressBarVisible(true)
        calendarViewModel = ViewModelProviders.of(this).get(CalendarViewModel::class.java)
        calendarViewModel.initialize(this.context!!)
        calendarViewModel.getEventList().observe(this, Observer {
            adapter.notifyDataSetChanged()
            if (it.count() > 0)
                initRecyclerView()
                setProgressBarVisible(false)
        })
        initRecyclerView()
        // Inflate the layout for this fragment
        return view
    }

    override fun onResume() {
        super.onResume()
    }

    private fun initRecyclerView() {
        adapter = CalendarAdapter(
            this.context!!, calendarViewModel.getItemList()!!
        )
        recyclerView.adapter = adapter
        recyclerView.layoutManager = LinearLayoutManager(this.context)
    }

    private fun setProgressBarVisible(visible: Boolean) {
        if (visible)
        {
            Log.v("Progress", "show")
            loadingBar.visibility = View.VISIBLE
        }

        else
            loadingBar.visibility = View.GONE
    }


    // TODO: Rename method, update argument and hook method into UI event
    fun onButtonPressed(uri: Uri) {
        listener?.onFragmentInteraction(uri)

    }


}
