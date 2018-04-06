package ru.lod_misis.user.eduhub.Models.Notivications;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

/**
 * Created by User on 06.04.2018.
 */

public class SunctionApplied {
    @SerializedName("BrokenRule")
    @Expose
    private String brokenRule;
    @SerializedName("SanctionType")
    @Expose
    private String sanctionType;

    public String getBrokenRule() {
        return brokenRule;
    }

    public void setBrokenRule(String brokenRule) {
        this.brokenRule = brokenRule;
    }

    public String getSanctionType() {
        return sanctionType;
    }

    public void setSanctionType(String sanctionType) {
        this.sanctionType = sanctionType;
    }
}
