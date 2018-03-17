package com.example.user.eduhub.Retrofit;

import com.example.user.eduhub.Models.AddFileResponseModel;
import com.example.user.eduhub.Models.AddPlanModel;
import com.example.user.eduhub.Models.AddReviewModel;
import com.example.user.eduhub.Models.ChangeInvitationStatusModel;
import com.example.user.eduhub.Models.CreateGroupModel;
import com.example.user.eduhub.Models.CreateGroupResponse;
import com.example.user.eduhub.Models.Group.GetAllGroups;
import com.example.user.eduhub.Models.Group.GetGroupsModel;
import com.example.user.eduhub.Models.Group.Group;
import com.example.user.eduhub.Models.Group.RefactorGroupRequestModel;
import com.example.user.eduhub.Models.GroupChangeInviteStatusResponse;
import com.example.user.eduhub.Models.InvitationResponse;
import com.example.user.eduhub.Models.InviteUserModel;
import com.example.user.eduhub.Models.LoginModel;
import com.example.user.eduhub.Models.Registration.RegistrationModel2;
import com.example.user.eduhub.Models.SearchModel;
import com.example.user.eduhub.Models.User;
import com.example.user.eduhub.Models.Registration.RegistrationModel;
import com.example.user.eduhub.Models.Registration.RegistrationResponseModel;
import com.example.user.eduhub.Models.UserProfile.RefactorUserRequestModel;
import com.example.user.eduhub.Models.UserProfile.UserProfileResponse;
import com.example.user.eduhub.Models.UserProfile.UserSearchProfileResponse;
import com.example.user.eduhub.Models.UsersResponseModel;
import com.example.user.eduhub.Models.getFileFromServer;


import java.io.File;

import io.reactivex.Completable;
import io.reactivex.Observable;
import io.reactivex.Single;
import okhttp3.MultipartBody;
import okhttp3.RequestBody;
import okhttp3.ResponseBody;
import retrofit2.http.Body;
import retrofit2.http.DELETE;
import retrofit2.http.GET;
import retrofit2.http.Header;
import retrofit2.http.Multipart;
import retrofit2.http.POST;
import retrofit2.http.PUT;
import retrofit2.http.Part;
import retrofit2.http.Path;
import retrofit2.http.Query;
import retrofit2.http.Url;

/**
 * Created by user on 16.12.2017.
 */

public interface EduHubApi {
    @POST("/api/account/registration")
    Observable<RegistrationResponseModel> userRegistration(@Body RegistrationModel registrationModel);
    @POST("/api/account/registration")
    Observable<RegistrationResponseModel> userRegistrationWithoutInviteCode(@Body RegistrationModel2 registrationModel);
    @POST("/api/account/login")
    Single<User> userLogin(@Body LoginModel loginModel);
    @GET("/api/group")
    Observable<GetAllGroups> getGroups();
    @GET(" /api/group/{groupId}")
    Observable<Group> getInformationAbotGroup(@Path("groupId")String id);
    @POST("/api/group/{groupId}/member/invitation")
    Completable invitedUser(@Header("Authorization") String token, @Path("groupId") String groupId, @Body InviteUserModel model);
    @POST("/api/group/{groupId}/teacher/invitation")
    Completable invitedTeacher(@Header("Authorization") String token, @Path("groupId") String groupId, @Body InviteUserModel model);
    @GET("/api/user/profile/groups/{userId}")
    Observable<GetGroupsModel> getUsersGroup(@Header("Authorization") String token,@Path("userId") String userId);
    @POST("/api/group")
    Single<CreateGroupResponse> createGroup(@Header("Authorization") String token, @Body CreateGroupModel model);
    @GET("/api/user/profile/{userId}")
    Single<UserProfileResponse> getUsersProfile (@Header("Authorization") String token, @Path("userId") String userId);
    @POST("/api/users/search")
    Single<UserSearchProfileResponse> searchUser (@Body SearchModel model);
    @POST("/api/users/searchForInvitation")
    Single<UserSearchProfileResponse> searchUserForInvitation (@Body SearchModel model);
    @POST("/api/group/{groupId}/member")
    Completable signInUserToGroup (@Header("Authorization") String token, @Path("groupId") String groupId);
    @GET("/api/user/profile/invitations")
    Observable<InvitationResponse> getInvitations(@Header("Authorization") String token);
    @PUT("/api/user/profile/invitations")
    Single<GroupChangeInviteStatusResponse> changeStatusOfInvitation(@Header("Authorization") String token, @Body ChangeInvitationStatusModel model);
    @PUT("/api/user/profile/name")
    Completable changesUserName(@Header("Authorization") String token, @Body RefactorUserRequestModel model);
    @PUT("/api/user/profile/about")
    Completable changesAboutUser(@Header("Authorization") String token, @Body RefactorUserRequestModel model);
    @PUT("/api/user/profile/avatar")
    Completable changesUserAvatar(@Header("Authorization") String token, @Body RefactorUserRequestModel model);
    @PUT("/api/user/profile/contacts")
    Completable changesUsersContacts(@Header("Authorization") String token, @Body RefactorUserRequestModel model);
    @PUT("/api/user/profile/birthYear")
    Completable changesUsersBirthYear(@Header("Authorization") String token, @Body RefactorUserRequestModel model);
    @PUT("/api/user/profile/gender")
    Completable changesUsersGender(@Header("Authorization") String token, @Body RefactorUserRequestModel model);
    @DELETE("/api/group/{groupId}/member/{memberId}")
    Completable exitFromGroup(@Header("Authorization") String token,@Path("groupId") String groupId,@Path("memberId") String userId);
    @DELETE("/api/group/{groupId}/member/teacher/{memberId}")
    Completable exitFromGroupForTeacher(@Header("Authorization") String token,@Path("groupId") String groupId,@Path("memberId") String userId);
    @DELETE("/api/user/profile/teaching")
    Completable becomeSimpleUser(@Header("Authorization") String token);
    @POST("/api/user/profile/teaching")
    Completable becomeTeacher(@Header("Authorization") String token);
    @PUT("/api/group/{groupId}/title")
    Completable changeGroupTitle(@Header("Authorization") String token,@Path("groupId") String groupId,@Body RefactorGroupRequestModel model);
    @PUT("/api/group/{groupId}/description")
   Completable changeGroupDescription(@Header("Authorization") String token,@Path("groupId") String groupId,@Body RefactorGroupRequestModel model);
    @PUT("/api/group/{groupId}/tags")
    Completable changeGroupTags(@Header("Authorization") String token,@Path("groupId") String groupId,@Body RefactorGroupRequestModel model);
    @PUT("/api/group/{groupId}/size")
    Completable changeGroupSize(@Header("Authorization") String token,@Path("groupId") String groupId,@Body RefactorGroupRequestModel model);
    @PUT("/api/group/{groupId}/price")
    Completable changeGroupPrice(@Header("Authorization") String token,@Path("groupId") String groupId,@Body RefactorGroupRequestModel model);
    @PUT("/api/group/{groupId}/type")
    Completable changeGroupType(@Header("Authorization") String token,@Path("groupId") String groupId,@Body RefactorGroupRequestModel model);
    @PUT("/api/group/{groupId}/privacy")
    Completable changeGroupPrivacy(@Header("Authorization") String token,@Path("groupId") String groupId,@Body RefactorGroupRequestModel model);
    @GET("/api/account/refresh")
    Single<User> refreshToken(@Header("Authorization") String token);
    @POST("/api/group/{groupId}/course/curriculum")
    Completable addPlanForStudy(@Header("Authorization") String token, @Path("groupId") String groupId,@Body AddPlanModel addPlanModel);
    @PUT("/api/group/{groupId}/course/curriculum")
    Completable positiveResponse(@Header("Authorization") String token,@Path("groupId") String groupId);
    @DELETE("/api/group/{groupId}/course/curriculum")
    Completable negativeResponse(@Header("Authorization") String token, @Path("groupId") String groupId, UsersResponseModel model);
    @Multipart
    @POST("/api/file")
    Observable<AddFileResponseModel> loadFiletoServer(@Header("Authorization") String token,
                                                      @Part("file") RequestBody description,
                                                      @Part MultipartBody.Part file);
    @GET("/api/file/{filename}")
    Observable<ResponseBody> loafFileFromServer(@Header("Authorization") String token, @Path("filename") String fileName);
    @POST("/api/group/{groupId}/course/review")
    Completable addReview(@Header("Authorization") String token, @Path("groupId") String groupId,@Body AddReviewModel addReviewModel);
    @DELETE(" /api/group/{groupId}/course")
    Completable closeCourse(@Header("Authorization") String token, @Path("groupId") String groupId);
}
