package ru.lod_misis.user.eduhub.Classes;

import java.io.Serializable;
import java.util.ArrayList;

/**
 * Created by User on 13.04.2018.
 */

public class  FiltresModel implements Serializable{
    private String type;
    private Double minCost;
    private  Double maxCost;
    private ArrayList<String> tags;
    private String tittle;
    private Boolean privacy;
    private static FiltresModel instance;

    public FiltresModel(String type, Double minCost, Double maxCost, ArrayList<String> tags, String tittle, Boolean privacy) {
        this.type = type;
        this.minCost = minCost;
        this.maxCost = maxCost;
        this.tags = tags;
        this.tittle = tittle;
        this.privacy = privacy;
    }

    public static synchronized FiltresModel getInstance(){
        if(instance==null){
            instance=new FiltresModel("",0.0,0.0,new ArrayList<>(),"Default",false);
        }
        return instance;
    }

    public Boolean getPrivacy() {
        return privacy;
    }

    public void setPrivacy(Boolean privacy) {
        this.privacy = privacy;
    }

    public String getTittle() {
        return tittle;
    }

    public void setTittle(String tittle) {
        this.tittle = tittle;
    }

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
    }

    public Double getMinCost() {
        return minCost;
    }

    public void setMinCost(Double minCost) {
        this.minCost = minCost;
    }

    public Double getMaxCost() {
        return maxCost;
    }

    public void setMaxCost(Double maxCost) {
        this.maxCost = maxCost;
    }

    public ArrayList<String> getTags() {
        return tags;
    }

    public void setTags(ArrayList<String> tags) {
        this.tags = tags;
    }
}
