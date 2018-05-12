package ru.lod_misis.user.eduhub.Retrofit;

import java.util.ArrayList;
import java.util.List;

import retrofit2.http.Query;
import ru.lod_misis.user.eduhub.Models.AddFileResponseModel;
import ru.lod_misis.user.eduhub.Models.AddPlanModel;
import ru.lod_misis.user.eduhub.Models.AddReviewModel;
import ru.lod_misis.user.eduhub.Models.ChangeInvitationStatusModel;
import ru.lod_misis.user.eduhub.Models.CreateGroupModel;
import ru.lod_misis.user.eduhub.Models.CreateGroupResponse;
import ru.lod_misis.user.eduhub.Models.Group.GetAllGroups;
import ru.lod_misis.user.eduhub.Models.Group.GetGroupsModel;
import ru.lod_misis.user.eduhub.Models.Group.Group;
import ru.lod_misis.user.eduhub.Models.Group.Message;
import ru.lod_misis.user.eduhub.Models.Group.NewMessage;
import ru.lod_misis.user.eduhub.Models.Group.RefactorGroupRequestModel;
import ru.lod_misis.user.eduhub.Models.GroupChangeInviteStatusResponse;
import ru.lod_misis.user.eduhub.Models.InvitationResponse;
import ru.lod_misis.user.eduhub.Models.InviteUserModel;
import ru.lod_misis.user.eduhub.Models.LoginModel;
import ru.lod_misis.user.eduhub.Models.Notivications.Notifications;
import ru.lod_misis.user.eduhub.Models.Registration.RegistrationModel2;
import ru.lod_misis.user.eduhub.Models.SearchModel;
import ru.lod_misis.user.eduhub.Models.SendMessageResponseModel;
import ru.lod_misis.user.eduhub.Models.Tag;
import ru.lod_misis.user.eduhub.Models.User;
import ru.lod_misis.user.eduhub.Models.Registration.RegistrationModel;
import ru.lod_misis.user.eduhub.Models.Registration.RegistrationResponseModel;
import ru.lod_misis.user.eduhub.Models.UserProfile.ChangedSkilsRequestModel;
import ru.lod_misis.user.eduhub.Models.UserProfile.RefactorUserRequestModel;
import ru.lod_misis.user.eduhub.Models.UserProfile.UserProfileResponse;
import ru.lod_misis.user.eduhub.Models.UserProfile.UserSearchProfileResponse;
import ru.lod_misis.user.eduhub.Models.UsersResponseModel;


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
    Observable<Group> getInformationAbotGroup(@Path("groupId") String id);

    @POST("/api/group/{groupId}/member/invitation")
    Completable invitedUser(@Header("Authorization") String token, @Path("groupId") String groupId, @Body InviteUserModel model);

    @POST("/api/group/{groupId}/teacher/invitation")
    Completable invitedTeacher(@Header("Authorization") String token, @Path("groupId") String groupId, @Body InviteUserModel model);

    @GET("/api/user/profile/groups/{userId}")
    Observable<GetGroupsModel> getUsersGroup(@Header("Authorization") String token, @Path("userId") String userId);

    @POST("/api/group")
    Single<CreateGroupResponse> createGroup(@Header("Authorization") String token, @Body CreateGroupModel model);

    @GET("/api/user/profile/{userId}")
    Single<UserProfileResponse> getUsersProfile(@Header("Authorization") String token, @Path("userId") String userId);

    @POST("/api/users/search")
    Single<UserSearchProfileResponse> searchUser(@Body SearchModel model);

    @POST("/api/users/searchForInvitation")
    Single<UserSearchProfileResponse> searchUserForInvitation(@Body SearchModel model);

    @POST("/api/group/{groupId}/member")
    Completable signInUserToGroup(@Header("Authorization") String token, @Path("groupId") String groupId);

    @GET("/api/user/profile/invitations")
    Observable<InvitationResponse> getInvitations(@Header("Authorization") String token);

    @PUT("/api/user/profile/invitations")
    Single<GroupChangeInviteStatusResponse> changeStatusOfInvitation(@Header("Authorization") String token, @Body ChangeInvitationStatusModel model);

    @PUT("/api/user/profile")
    Completable changesProfile(@Header("Authorization") String token, @Body RefactorUserRequestModel model);

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
    Completable exitFromGroup(@Header("Authorization") String token, @Path("groupId") String groupId, @Path("memberId") String userId);

    @DELETE(" /api/group/{groupId}/teacher")
    Completable exitFromGroupForTeacher(@Header("Authorization") String token, @Path("groupId") String groupId);

    @DELETE("/api/user/profile/teaching")
    Completable becomeSimpleUser(@Header("Authorization") String token);

    @POST("/api/user/profile/teaching")
    Completable becomeTeacher(@Header("Authorization") String token);

    @PUT("/api/group/{groupId}/title")
    Completable changeGroupTitle(@Header("Authorization") String token, @Path("groupId") String groupId, @Body RefactorGroupRequestModel model);

    @PUT("/api/group/{groupId}/description")
    Completable changeGroupDescription(@Header("Authorization") String token, @Path("groupId") String groupId, @Body RefactorGroupRequestModel model);

    @PUT("/api/group/{groupId}/tags")
    Completable changeGroupTags(@Header("Authorization") String token, @Path("groupId") String groupId, @Body RefactorGroupRequestModel model);

    @PUT("/api/group/{groupId}/size")
    Completable changeGroupSize(@Header("Authorization") String token, @Path("groupId") String groupId, @Body RefactorGroupRequestModel model);

    @PUT("/api/group/{groupId}/price")
    Completable changeGroupPrice(@Header("Authorization") String token, @Path("groupId") String groupId, @Body RefactorGroupRequestModel model);

    @PUT("/api/group/{groupId}/type")
    Completable changeGroupType(@Header("Authorization") String token, @Path("groupId") String groupId, @Body RefactorGroupRequestModel model);

    @PUT("/api/group/{groupId}/privacy")
    Completable changeGroupPrivacy(@Header("Authorization") String token, @Path("groupId") String groupId, @Body RefactorGroupRequestModel model);

    @GET("/api/account/refresh")
    Single<User> refreshToken(@Header("Authorization") String token);

    @POST("/api/group/{groupId}/course/curriculum")
    Completable addPlanForStudy(@Header("Authorization") String token, @Path("groupId") String groupId, @Body AddPlanModel addPlanModel);

    @PUT("/api/group/{groupId}/course/curriculum")
    Completable positiveResponse(@Header("Authorization") String token, @Path("groupId") String groupId);

    @DELETE("/api/group/{groupId}/course/curriculum")
    Completable negativeResponse(@Header("Authorization") String token, @Path("groupId") String groupId, UsersResponseModel model);

    @Multipart
    @POST("/api/file")
    Observable<AddFileResponseModel> loadFiletoServer(@Header("Authorization") String token,
                                                      @Part("file") RequestBody description,
                                                      @Part MultipartBody.Part file);

    @GET("/api/file/{filename}")
    Observable<ResponseBody> loafFileFromServer(@Header("Authorization") String token, @Path("filename") String fileName);

    @GET("/api/file/img/{filename}")
    Observable<ResponseBody> loadImageFromServer( @Path("filename") String fileName);

    @POST("/api/group/{groupId}/course/review")
    Completable addReview(@Header("Authorization") String token, @Path("groupId") String groupId, @Body AddReviewModel addReviewModel);

    @DELETE(" /api/group/{groupId}/course")
    Completable closeCourse(@Header("Authorization") String token, @Path("groupId") String groupId);

    @POST("/api/group/{groupId}/teacher")
    Completable signInTeacherToGroup(@Header("Authorization") String token, @Path("groupId") String groupId);

    @GET("/api/group/search")
    Observable<List<Group>> findGroupsWithFilters(@Query("minPrice") double minPrice, @Query("maxPrice") double maxPrice,
                                                  @Query("title") String title, @Query("tags") ArrayList<String> tags,
                                                  @Query("type") String type, @Query("formed") Boolean formed);

    @GET("/api/group/search")
    Observable<List<Group>> findGroupsWithOutFilters(@Query("title") String title);

    @GET("/api/group/search")
    Observable<List<Group>> findGroupsWithFilters1(@Query("title") String title);

    @GET("api/user/profile/notifications")
    Observable<List<Notifications>> loadAllNotifications(@Header("Authorization") String token);

    @GET("/api/group/{groupId}/chat")
    Observable<List<Message>> loadAllMessages(@Header("Authorization") String token, @Path("groupId") String groupId);

    @POST("/api/group/{groupId}/chat")
    Single<SendMessageResponseModel> sendMessage(@Header("Authorization") String token, @Path("groupId") String groupId, @Body NewMessage message);

    @PUT("/api/user/profile/teaching/skills")
    Completable changedSkils(@Header("Authorization") String token, @Body ChangedSkilsRequestModel model);

    @GET("/api/tags/search")
    Single<ArrayList<Tag>> findTags(@Query("tag") String tag);
}