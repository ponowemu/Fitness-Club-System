package pl.smartica.trimfitmobile.model

import java.math.BigDecimal


class Service(header: String,description: String) {
    var serviceId: Int? = null
    var serviceHeader: String? = null
    var serviceDescription: String? = null
    var servicePriceGross: BigDecimal = BigDecimal.ZERO
    var serviceImageUrl: String? = null

    init{
        this.serviceHeader = header
        this.serviceDescription = description
    }
}