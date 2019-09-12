package pl.smartica.trimfitmobile.model

class DayEvents(mDay: String, wDay: String, events: MutableList<SingleDayEvent>) {
    var monthDay: String
    var weekDay: String
    var dayEvents: MutableList<SingleDayEvent>

    init {
        monthDay = mDay
        weekDay = wDay
        dayEvents = events
    }
}