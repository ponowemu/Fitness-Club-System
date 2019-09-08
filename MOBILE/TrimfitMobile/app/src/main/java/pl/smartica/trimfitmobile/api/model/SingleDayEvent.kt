package pl.smartica.trimfitmobile.api.model

import java.time.LocalDateTime

class SingleDayEvent(name: String, start: LocalDateTime, end: LocalDateTime) {
    var eventName: String? = null
    var startTime: LocalDateTime? = null
    var endTime: LocalDateTime? = null

    init {
        eventName = name
        startTime = start
        endTime = end
    }
}