package ru.lod_misis.user.eduhub.Models.Notivications;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

/**
 * Created by User on 06.04.2018.
 */

public class Notifications {
    @SerializedName("id")
    @Expose
    private String id;
    @SerializedName("occurredOn")
    @Expose
    private String occurredOn;
    @SerializedName("eventInfo")
    @Expose
    private String eventInfo;
    @SerializedName("eventType")
    @Expose
    private Integer eventType;

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getOccurredOn() {
        return occurredOn;
    }

    public void setOccurredOn(String occurredOn) {
        this.occurredOn = occurredOn;
    }

    public String getEventInfo() {
        return eventInfo;
    }

    public void setEventInfo(String eventInfo) {
        this.eventInfo = eventInfo;
    }

    public Integer getEventType() {
        return eventType;
    }

    public void setEventType(Integer eventType) {
        this.eventType = eventType;
    }
}
