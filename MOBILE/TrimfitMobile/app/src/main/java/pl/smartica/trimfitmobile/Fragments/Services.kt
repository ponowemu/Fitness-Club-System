package pl.smartica.trimfitmobile.Fragments

import android.content.Context
import android.net.Uri
import android.os.Bundle
import android.util.Log
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.android.volley.Request
import com.android.volley.Response
import com.android.volley.toolbox.JsonArrayRequest
import org.json.JSONArray
import pl.smartica.trimfitmobile.Callback
import pl.smartica.trimfitmobile.R
import pl.smartica.trimfitmobile.ServiceAdapter
import pl.smartica.trimfitmobile.Tester
import pl.smartica.trimfitmobile.model.Service


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
    // TODO: Rename and change types of parameters
    private var param1: String? = null
    private var param2: String? = null
    private var listener: OnFragmentInteractionListener? = null

    var serviceList: MutableList<Service>? = null
    var adapter:ServiceAdapter? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        arguments?.let {
            param1 = it.getString(ARG_PARAM1)
            param2 = it.getString(ARG_PARAM2)
        }
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        val view = inflater.inflate(R.layout.fragment_services, container, false)
        serviceList= mutableListOf()
        adapter = ServiceAdapter(this.context!!,serviceList!!)
        val recycler = view.findViewById<RecyclerView>(R.id.service_recycler)
        recycler.adapter = adapter
        recycler.layoutManager = LinearLayoutManager(this.context)
        sendRequest(this.context!!)
        // Inflate the layout for this fragment
        return view
    }
    fun TestButton(view: View)
    {
        Log.v("Service","Click")
    }
    public fun sendRequest(context: Context)
    {
        // Instantiate the RequestQueue.
        val queue = Callback.getInstance(context).requestQueue
        val url = "http://api.trimfit.pl/api/Services"
        val stringReq = JsonArrayRequest(Request.Method.GET,url,null,
            Response.Listener<JSONArray> {
                    response ->setRecylerView(response)
                Log.d("RESPONE",response.toString()) },
            Response.ErrorListener {
                    error ->  Log.v("TAG", "ERROR LUL: " + error.toString())
            })
        Callback.getInstance(context).addToRequestQueue(stringReq)
    }
    fun setRecylerView(jsonArray: JSONArray){
        val view = LayoutInflater.from(this.context).inflate(R.layout.fragment_services, null, false)

        if (jsonArray != null)
        {
            for (item in 0..jsonArray!!.length()-1)
            {
                serviceList!!.add(Service(jsonArray.getJSONObject(item).getString("service_Name"),jsonArray.getJSONObject(item).getString("service_Description")))
            }
        }
        adapter!!.notifyDataSetChanged();
   //     Log.v("Services",getServicedData())


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

    companion object {
        /**
         * Use this factory method to create a new instance of
         * this fragment using the provided parameters.
         *
         * @param param1 Parameter 1.
         * @param param2 Parameter 2.
         * @return A new instance of fragment Services.
         */
        // TODO: Rename and change types and number of parameters
        @JvmStatic
        fun newInstance(param1: String, param2: String) =
            Services().apply {
                arguments = Bundle().apply {
                    putString(ARG_PARAM1, param1)
                    putString(ARG_PARAM2, param2)
                }
            }
    }
}
