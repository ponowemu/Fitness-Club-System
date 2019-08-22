package pl.smartica.trimfitmobile.model

import org.json.JSONObject
import java.math.BigDecimal


class Service {
    var serviceId: Int? = null
    var serviceHeader: String? = null
    var serviceDescription: String? = null
    var servicePriceGross: BigDecimal = BigDecimal.ZERO
    var servicePriceNet: BigDecimal = BigDecimal.ZERO
    var serviceImageUrl: String? = null
    var serviceDuration: Int? = null
    var serviceEmployees: MutableList<Int> = mutableListOf()

    constructor(header: String, description: String){
        this.serviceHeader = header
        this.serviceDescription = description
    }

    constructor(id: Int? ,
                header: String,
                description: String,
                imageUrl:String? = null,
                priceGross:BigDecimal = BigDecimal.ZERO,
                priceNet: BigDecimal = BigDecimal.ZERO) {
        serviceId = id
        serviceHeader = header
        serviceDescription = description
        serviceImageUrl = imageUrl
        servicePriceGross = priceGross
        servicePriceNet = priceNet
    }
    constructor(item: JSONObject) {
        serviceId = item.getString("service_Id").toInt()
        serviceHeader = item.getString("service_Name")
        serviceDescription = item.getString("service_Description")
        serviceImageUrl = item.getString("service_Photo_Url")
        servicePriceGross = item.getString("service_Gross_Price").toBigDecimal()
        servicePriceNet = item.getString("service_Net_Price").toBigDecimal()
        serviceDuration = item.getString("service_Duration").toInt()
        serviceEmployees = mutableListOf()
    }
}