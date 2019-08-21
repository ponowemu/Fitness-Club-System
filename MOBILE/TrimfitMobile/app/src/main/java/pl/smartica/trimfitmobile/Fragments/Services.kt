package pl.smartica.trimfitmobile.Fragments

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
import pl.smartica.trimfitmobile.ServiceAdapter
import pl.smartica.trimfitmobile.viewmodels.ServicesViewModel


// TODO: Rename parameter arguments, choose names that match
// the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
private const val ARG_PARAM1 = "param1"
private const val ARG_PARAM2 = "param2"

/**
 * A simple [Fragment] subclass.
 * Activities that contain this fragment must implement the
 * [Services.OnFragmentInteractionListener] interface
 * to handle interaction events.
 * Use the [Services.newInstance] factory method to
 * create an instance of this fragment.
 *
 */
class Services : Fragment() {
    private var listener: OnFragmentInteractionListener? = null

    //MVVM approach:
    lateinit var mAdapter: ServiceAdapter
    lateinit var mRecyclerView: RecyclerView
    lateinit var loadingBar: ProgressBar
    lateinit var mServicesViewModel: ServicesViewModel

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        val view = inflater.inflate(R.layout.fragment_services, container, false)
        loadingBar = view.findViewById(R.id.loadingBar)
        mRecyclerView = view.findViewById<RecyclerView>(R.id.service_recycler)

        setProgressBarVisible(true)
        mServicesViewModel = ViewModelProviders.of(this).get(ServicesViewModel::class.java)
        mServicesViewModel.initialize(this.context!!)

        mServicesViewModel.getServicesList().observe(this, Observer {
            mAdapter.notifyDataSetChanged()
            if (it.count() > 0)
                setProgressBarVisible(false)
        })
        Log.v("BEFORE", "BEFORE INIT")
        initRecyclerView()
        return view
    }

    private fun setProgressBarVisible(visible: Boolean) {
        if (visible)
            loadingBar.visibility = View.VISIBLE
        else
            loadingBar.visibility = View.GONE
    }

    private fun initRecyclerView() {
        mAdapter = ServiceAdapter(this.context!!, mServicesViewModel.getServicesList().value!!)
        mRecyclerView.adapter = mAdapter
        mRecyclerView.layoutManager = LinearLayoutManager(this.context)
    }

    // TODO: Rename method, update argument and hook method into UI event
    fun onButtonPressed(uri: Uri) {
        listener?.onFragmentInteraction(uri)
    }

    /**
     * This interface must be implemented by activities that contain this
     * fragment to allow an interaction in this fragment to be communicated
     * to the activity and potentially other fragments contained in that
     * activity.
     *
     *
     * See the Android Training lesson [Communicating with Other Fragments]
     * (http://developer.android.com/training/basics/fragments/communicating.html)
     * for more information.
     */
    interface OnFragmentInteractionListener {
        // TODO: Update argument type and name
        fun onFragmentInteraction(uri: Uri)
    }

}
