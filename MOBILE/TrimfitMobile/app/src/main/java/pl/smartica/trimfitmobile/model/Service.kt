package pl.smartica.trimfitmobile.model


class Service(header: String,description: String) {
    var serviceId: Int? = null
    var serviceHeader: String? = null
    var serviceDescription: String? = null
    var serviceImageUrl: String? = null

    init{
        this.serviceHeader = header
        this.serviceDescription = description
    }
}