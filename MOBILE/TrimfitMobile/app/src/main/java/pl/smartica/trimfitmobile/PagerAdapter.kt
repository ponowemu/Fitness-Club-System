package pl.smartica.trimfitmobile

import androidx.fragment.app.Fragment
import androidx.fragment.app.FragmentManager
import androidx.fragment.app.FragmentPagerAdapter
import androidx.fragment.app.FragmentStatePagerAdapter

class PagerAdapter(fm: FragmentManager?) : FragmentStatePagerAdapter(fm)
{
    var currentFragment: Fragment? = null
    var currentTitle: String = ""
    override fun getCount(): Int {
        if (currentFragment == null)
            return 0
        else
            return 1
    }

    override fun getItem(position: Int): Fragment {
        return currentFragment!!
    }

    fun setItem(fragment: Fragment, title: String){
        currentFragment = fragment
        currentTitle =  title
        notifyDataSetChanged()
    }

}