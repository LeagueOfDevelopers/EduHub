package com.example.user.eduhub.Retrofit;

import com.example.user.eduhub.Models.ChangeInvitationStatusModel;
import com.example.user.eduhub.Models.CreateGroupModel;
import com.example.user.eduhub.Models.CreateGroupResponse;
import com.example.user.eduhub.Models.Group.GetGroupsModel;
import com.example.user.eduhub.Models.Group.Group;
import com.example.user.eduhub.Models.InvitationResponse;
import com.example.user.eduhub.Models.InviteUserModel;
import com.example.user.eduhub.Models.LoginModel;
import com.example.user.eduhub.Models.SearchModel;
import com.example.user.eduhub.Models.User;
import com.example.user.eduhub.Models.Registration.RegistrationModel;
import com.example.user.eduhub.Models.Registration.RegistrationResponseModel;
import com.example.user.eduhub.Models.UserProfile.UserProfile;
import com.example.user.eduhub.Models.UserProfile.UserProfileResponse;


import io.reactivex.Completable;
import io.reactivex.Observable;
import io.reactivex.Single;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.Header;
import retrofit2.http.POST;
import retrofit2.http.PUT;
import retrofit2.http.Path;

/**
 * Created by user on 16.12.2017.
 */

public interface EduHubApi {
    @POST("/api/account/registration")
    Observable<RegistrationResponseModel> userRegistration(@Body RegistrationModel registrationModel);
    @POST("/api/account/login")
    Single<User> userLogin(@Body LoginModel loginModel);
    @GET("/api/group")
    Observable<GetGroupsModel> getGroups();
    @GET("/api/group/{idOfGroup}")
    Observable<Group> getInformationAbotGroup(@Path("idOfGroup")String id);
    @POST("/api/group/{groupId}/member/invitation")
    Single<String> invitedUser(@Header("Authorization") String token, @Path("groupId") String groupId, @Body InviteUserModel model);
    @GET("/api/user/profile/groups")
    Observable<GetGroupsModel> getUsersGroup(@Header("Authorization") String token);
    @POST("/api/group")
    Single<CreateGroupResponse> createGroup(@Header("Authorization") String token, @Body CreateGroupModel model);
    @GET("/api/user/profile")
    Single<UserProfile> getUsersProfile (@Header("Authorization") String token);
    @POST("/api/users/search")
    Single<UserProfileResponse> searchUser (@Body SearchModel model);
    @POST("/api/group/{groupId}/member")
    Completable signInUserToGroup (@Header("Authorization") String token, @Path("groupId") String groupId);
    @GET("/api/user/profile/invitations")
    Observable<InvitationResponse> getInvitations(@Header("Authorization") String token);
    @PUT("/api/user/profile/invitations")
    Single<String> changeStatusOfInvitation(@Header("Authorization") String token, @Body ChangeInvitationStatusModel model);

}
