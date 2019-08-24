package pl.smartica.trimfitmobile.ui.activities

import android.os.Bundle
import com.google.android.material.floatingactionbutton.FloatingActionButton
import com.google.android.material.snackbar.Snackbar
import androidx.core.view.GravityCompat
import androidx.appcompat.app.ActionBarDrawerToggle
import android.view.MenuItem
import androidx.drawerlayout.widget.DrawerLayout
import com.google.android.material.navigation.NavigationView
import androidx.appcompat.app.AppCompatActivity
import androidx.appcompat.widget.Toolbar
import android.view.Menu
import androidx.fragment.app.Fragment
import androidx.viewpager.widget.ViewPager
import pl.smartica.trimfitmobile.ui.fragments.*
import pl.smartica.trimfitmobile.PagerAdapter
import pl.smartica.trimfitmobile.R

class MainActivity : AppCompatActivity(), NavigationView.OnNavigationItemSelectedListener {

    public val xddd = ""
    private var pagerAdapter: PagerAdapter? = null
    private var viewPager: ViewPager? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        val toolbar: Toolbar = findViewById(R.id.toolbar)
        setSupportActionBar(toolbar)
        val fab: FloatingActionButton = findViewById(R.id.fab)
        fab.setOnClickListener { view ->
            Snackbar.make(view, "Replace with your own action", Snackbar.LENGTH_LONG)
                .setAction("Action", null).show()
        }
        val drawerLayout: DrawerLayout = findViewById(R.id.drawer_layout)
        val navView: NavigationView = findViewById(R.id.nav_view)
        val toggle = ActionBarDrawerToggle(
            this, drawerLayout, toolbar,
            R.string.navigation_drawer_open,
            R.string.navigation_drawer_close
        )
        drawerLayout.addDrawerListener(toggle)
        toggle.syncState()

        navView.setNavigationItemSelectedListener(this)
       // statePagerAdapter= PagerAdapter(supportFragmentManager)
        pagerAdapter = PagerAdapter(supportFragmentManager)
        viewPager = findViewById(R.id.page_containter)
        viewPager?.adapter = pagerAdapter
        this.setCurrentPage(Dashboard(),"Dashboard")
    }
    private fun setCurrentPage(fragment: Fragment, title: String){
        pagerAdapter?.setItem(fragment,title)
        viewPager?.adapter = pagerAdapter
    }

    override fun onBackPressed() {
        val drawerLayout: DrawerLayout = findViewById(R.id.drawer_layout)
        if (drawerLayout.isDrawerOpen(GravityCompat.START)) {
            drawerLayout.closeDrawer(GravityCompat.START)
        } else {
            super.onBackPressed()
        }
    }

    override fun onCreateOptionsMenu(menu: Menu): Boolean {
        // Inflate the menu; this adds items to the action bar if it is present.
        menuInflater.inflate(R.menu.main, menu)
        return true
    }

    override fun onOptionsItemSelected(item: MenuItem): Boolean {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        return when (item.itemId) {
            R.id.action_settings -> true
            else -> super.onOptionsItemSelected(item)
        }
    }
    override fun onNavigationItemSelected(item: MenuItem): Boolean {
        // Handle navigation view item clicks here.
        when (item.itemId) {
            R.id.navigation_drawer_dashboard -> {
                setCurrentPage(Dashboard(),"Dashboard")
           //     this.setCurrentPage(statePagerAdapter?.fragmentTitle!!.indexOf("Dashboard"))
            }
            R.id.navigation_drawer_calendar -> {
                setCurrentPage(Calendar(),"Calendar")
           //     this.setCurrentPage(statePagerAdapter?.fragmentTitle!!.indexOf("Calendar"))
            }
            R.id.navigation_drawer_activities -> {
                setCurrentPage(Activities(),"Activites")
           //     this.setCurrentPage(statePagerAdapter?.fragmentTitle!!.indexOf("Activites"))
            }
            R.id.navigation_drawer_products -> {
                setCurrentPage(Products(),"Products")
          //      this.setCurrentPage(statePagerAdapter?.fragmentTitle!!.indexOf("Products"))
            }
            R.id.navigation_drawer_tickets -> {
                setCurrentPage(Tickets(),"Tickets")
          //      this.setCurrentPage(statePagerAdapter?.fragmentTitle!!.indexOf("Tickets"))
            }
            R.id.navigation_drawer_services -> {
                setCurrentPage(Services(),"Services")
          //      this.setCurrentPage(statePagerAdapter?.fragmentTitle!!.indexOf("Services"))
            }
        }
        val drawerLayout: DrawerLayout = findViewById(R.id.drawer_layout)
        drawerLayout.closeDrawer(GravityCompat.START)
        return true
    }
}
