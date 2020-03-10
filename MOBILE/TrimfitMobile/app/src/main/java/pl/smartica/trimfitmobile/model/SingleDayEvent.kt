package pl.smartica.trimfitmobile.model

import java.time.LocalDate
import java.time.LocalDateTime

class SingleDayEvent(name: String, start: LocalDateTime, end: LocalDateTime, colorBackground: String) {
    var eventName: String? = null
    var startTime: LocalDateTime? = null
    var endTime: LocalDateTime? = null
    var color: String? = null
    init {
        eventName = name
        startTime = start
        endTime = end
        color = colorBackground
    }
}