webpackJsonp([0],{"./app/components/Chat/Loadable.js":function(e,t,n){"use strict";var r=n("./node_modules/react-loadable/lib/index.js"),o=n.n(r);t.a=o()({loader:function(){return n.e(10).then(n.bind(null,"./app/components/Chat/index.js"))},loading:function(){return null}})},"./app/components/InviteMemberSelect/Loadable.js":function(e,t,n){"use strict";var r=n("./node_modules/react-loadable/lib/index.js"),o=n.n(r);t.a=o()({loader:function(){return n.e(8).then(n.bind(null,"./app/components/InviteMemberSelect/index.js"))},loading:function(){return null}})},"./app/components/MembersList/Loadable.js":function(e,t,n){"use strict";var r=n("./node_modules/react-loadable/lib/index.js"),o=n.n(r);t.a=o()({loader:function(){return n.e(9).then(n.bind(null,"./app/components/MembersList/index.js"))},loading:function(){return null}})},"./app/containers/GroupPage/actions.js":function(e,t,n){"use strict";function r(e){return{type:k.w,groupId:e}}function o(){return{type:k.x}}function a(e){return{type:k.v,error:e}}function i(e,t,n){return{type:k.C,groupId:e,memberId:t,role:n}}function s(){return{type:k.D}}function u(e){return{type:k.B,error:e}}function c(e,t,n){return{type:k.z,groupId:e,invitedId:t,role:n}}function p(){return{type:k.A}}function d(e){return{type:k.y,error:e}}function l(e,t){return{type:k.p,id:e,title:t}}function g(){return{type:k.r}}function f(e){return{type:k.q,error:e}}function h(e,t){return{type:k.a,id:e,description:t}}function m(){return{type:k.c}}function b(e){return{type:k.b,error:e}}function I(e,t){return{type:k.m,id:e,tags:t}}function v(){return{type:k.o}}function j(e){return{type:k.n,error:e}}function y(e,t){return{type:k.j,id:e,size:t}}function S(){return{type:k.l}}function O(e){return{type:k.k,error:e}}function C(e,t){return{type:k.d,id:e,price:t}}function _(){return{type:k.f}}function E(e){return{type:k.e,error:e}}function T(e,t){return{type:k.s,id:e,groupType:t}}function R(){return{type:k.u}}function x(e){return{type:k.t,error:e}}function P(e,t){return{type:k.g,id:e,isPrivate:t}}function D(){return{type:k.i}}function A(e){return{type:k.h,error:e}}function G(e,t){return{type:k.E,groupId:e,username:t}}function w(e){return{type:k.G,payload:e||[]}}function U(e){return{type:k.F,error:e}}t.v=r,t.x=o,t.w=a,t.B=i,t.D=s,t.C=u,t.y=c,t.A=p,t.z=d,t.m=l,t.o=g,t.n=f,t.a=h,t.c=m,t.b=b,t.j=I,t.l=v,t.k=j,t.g=y,t.i=S,t.h=O,t.d=C,t.f=_,t.e=E,t.p=T,t.r=R,t.q=x,t.s=P,t.u=D,t.t=A,t.E=G,t.G=w,t.F=U;var k=n("./app/containers/GroupPage/constants.js")},"./app/containers/GroupPage/constants.js":function(e,t,n){"use strict";n.d(t,"w",function(){return r}),n.d(t,"x",function(){return o}),n.d(t,"v",function(){return a}),n.d(t,"C",function(){return i}),n.d(t,"D",function(){return s}),n.d(t,"B",function(){return u}),n.d(t,"z",function(){return c}),n.d(t,"A",function(){return p}),n.d(t,"y",function(){return d}),n.d(t,"p",function(){return l}),n.d(t,"r",function(){return g}),n.d(t,"q",function(){return f}),n.d(t,"a",function(){return h}),n.d(t,"c",function(){return m}),n.d(t,"b",function(){return b}),n.d(t,"m",function(){return I}),n.d(t,"o",function(){return v}),n.d(t,"n",function(){return j}),n.d(t,"j",function(){return y}),n.d(t,"l",function(){return S}),n.d(t,"k",function(){return O}),n.d(t,"d",function(){return C}),n.d(t,"f",function(){return _}),n.d(t,"e",function(){return E}),n.d(t,"s",function(){return T}),n.d(t,"u",function(){return R}),n.d(t,"t",function(){return x}),n.d(t,"g",function(){return P}),n.d(t,"i",function(){return D}),n.d(t,"h",function(){return A}),n.d(t,"E",function(){return G}),n.d(t,"G",function(){return w}),n.d(t,"F",function(){return U});var r="ENTER_GROUP_START",o="ENTER_GROUP_SUCCESS",a="ENTER_GROUP_FAILED",i="LEAVE_GROUP_START",s="LEAVE_GROUP_SUCCESS",u="LEAVE_GROUP_FAILED",c="INVITE_MEMBER_START",p="INVITE_MEMBER_SUCCESS",d="INVITE_MEMBER_FAILED",l="EDIT_GROUP_TITLE",g="EDIT_GROUP_TITLE_SUCCESS",f="EDIT_GROUP_TITLE_FAILED",h="EDIT_GROUP_DESCRIPTION",m="EDIT_GROUP_DESCRIPTION_SUCCESS",b="EDIT_GROUP_DESCRIPTION_FAILED",I="EDIT_GROUP_TAGS",v="EDIT_GROUP_TAGS_SUCCESS",j="EDIT_GROUP_TAGS_FAILED",y="EDIT_GROUP_SIZE",S="EDIT_GROUP_SIZE_SUCCESS",O="EDIT_GROUP_SIZE_FAILED",C="EDIT_GROUP_PRICE",_="EDIT_GROUP_PRICE_SUCCESS",E="EDIT_GROUP_PRICE_FAILED",T="EDIT_GROUP_TYPE",R="EDIT_GROUP_TYPE_SUCCESS",x="EDIT_GROUP_TYPE_FAILED",P="EDIT_GROUP_PRIVACY",D="EDIT_GROUP_PRIVACY_SUCCESS",A="EDIT_GROUP_PRIVACY_FAILED",G="SEARCH_INVITATION_MEMBER",w="SEARCH_INVITATION_MEMBER_SUCCESS",U="SEARCH_INVITATION_MEMBER_FAILED"},"./app/containers/GroupPage/index.js":function(e,t,n){"use strict";Object.defineProperty(t,"__esModule",{value:!0}),function(e){function r(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}function o(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}function a(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}function i(e){return{enterGroup:function(t){return e(Object(b.v)(t))},leaveGroup:function(t,n,r){return e(Object(b.B)(t,n,r))},editGroupTitle:function(t,n){return e(Object(b.m)(t,n))},editGroupDescription:function(t,n){return e(Object(b.a)(t,n))},editGroupTags:function(t,n){return e(Object(b.j)(t,n))},editGroupSize:function(t,n){return e(Object(b.g)(t,n))},editGroupPrice:function(t,n){return e(Object(b.d)(t,n))},editPrivacy:function(t,n){return e(Object(b.s)(t,n))},editGroupType:function(t,n){return e(Object(b.p)(t,n))}}}n.d(t,"GroupPage",function(){return M});var s=n("./node_modules/react/react.js"),u=n.n(s),c=n("./node_modules/prop-types/index.js"),p=(n.n(c),n("./node_modules/react-redux/es/index.js")),d=n("./node_modules/reselect/es/index.js"),l=n("./node_modules/redux/es/index.js"),g=n("./app/utils/injectSaga.js"),f=n("./app/utils/injectReducer.js"),h=n("./app/containers/GroupPage/reducer.js"),m=n("./app/containers/GroupPage/saga.js"),b=n("./app/containers/GroupPage/actions.js"),I=n("./node_modules/react-router-dom/index.js"),v=(n.n(I),n("./app/config.js")),j=n("./app/globalJS.js"),y=n("./node_modules/antd/lib/index.js"),S=(n.n(y),n("./app/components/MembersList/Loadable.js")),O=n("./app/components/Chat/Loadable.js"),C=n("./app/components/InviteMemberSelect/Loadable.js"),_=n("./app/containers/SigningInForm/index.js"),E=function(){var e="function"==typeof Symbol&&Symbol.for&&Symbol.for("react.element")||60103;return function(t,n,r,o){var a=t&&t.defaultProps,i=arguments.length-3;if(n||0===i||(n={}),n&&a)for(var s in a)void 0===n[s]&&(n[s]=a[s]);else n||(n=a||{});if(1===i)n.children=o;else if(i>1){for(var u=Array(i),c=0;c<i;c++)u[c]=arguments[c+3];n.children=u}return{$$typeof:e,type:t,key:void 0===r?null:""+r,ref:null,props:n,_owner:null}}}(),T=function(){function e(e,t){for(var n=0;n<t.length;n++){var r=t[n];r.enumerable=r.enumerable||!1,r.configurable=!0,"value"in r&&(r.writable=!0),Object.defineProperty(e,r.key,r)}}return function(t,n,r){return n&&e(t.prototype,n),r&&e(t,r),t}}(),R={groupInfo:{isPrivate:!0,title:"Название группы",description:"Описание группы. Описание группы. Описание группы. Описание группы. Описание группы. Описание группы. Описание группы. Описание группы. Описание группы. Описание группы. Описание группы. Описание группы. Описание группы. Описание группы. Описание группы. Описание группы. Описание группы. Описание группы. Описание группы. Описание группы. Описание группы. Описание группы. Описание группы. Описание группы. Описание группы. ",isActive:!0,tags:["js","c#"],moneyPerUser:600,size:10,groupType:"Лекция"},members:[{member:{userId:"848a3202-7085-4cba-842f-07d07eff7b35",memberRole:3,paid:!0,acceptedCourse:!1},name:"Первый пользователь",avatarLink:"string"},{member:{userId:"string",memberRole:1,paid:!0,acceptedCourse:!1},name:"Второй пользователь",avatarLink:"string"}],educator:null},x=E(y.Select.Option,{value:"html"},void 0,"html"),P=E(y.Select.Option,{value:"css"},void 0,"css"),D=E(y.Select.Option,{value:"js"},void 0,"js"),A=E(y.Select.Option,{value:"c#"},void 0,"c#"),G=E(y.Col,{},void 0,"Формат"),w=E(y.Select.Option,{value:"Lecture"},void 0,"Лекция"),U=E(y.Select.Option,{value:"MasterClass"},void 0,"Мастер-класс"),k=E(y.Select.Option,{value:"Seminar"},void 0,"Семинар"),B=E(y.Col,{},void 0,"Стоимость"),z=E(y.Col,{span:16},void 0,E("label",{htmlFor:"privacy"},void 0,"Приватная группа")),L=E(y.Col,{},void 0,"Эта группа является приватной"),N=E(y.Col,{},void 0,"Эта группа не является приватной"),H=E(y.Col,{span:10},void 0,E("label",{htmlFor:"size"},void 0,"Участников")),M=function(t){function n(t){r(this,n);var a=o(this,(n.__proto__||Object.getPrototypeOf(n)).call(this,t));return a.onSignInClick=function(){a.setState({signInVisible:!0})},a.handleCancel=function(){a.setState({signInVisible:!1})},a.getCurrentGroup=function(){"true"!==localStorage.getItem("without_server")?e(v.a.API_BASE_URL+"/group/"+a.state.id,{headers:{"Content-Type":"application/json-patch+json",Authorization:"Bearer "+localStorage.getItem("token")}}).then(function(e){return e.json()}).then(function(e){return a.onSetResult(e)}).catch(function(e){return e}):a.onSetResult(R)},a.onChangeTitleHandle=function(e){a.setState({titleInput:e.target.value})},a.onChangeDescriptionHandle=function(e){a.setState({descInput:e.target.value})},a.onChangeSizeHandle=function(e){a.setState({sizeInput:e})},a.onChangePriceHandle=function(e){a.setState({priceInput:e})},a.onChangeTagsHandle=function(e){a.setState({tagsInput:e})},a.onHandleGroupTypeChange=function(e){a.setState({groupTypeInput:e})},a.onHandlePrivateChange=function(e){a.setState({privateInput:e})},a.cancelChanges=function(){a.setState({isEditing:!1,titleInput:a.state.groupData.groupInfo.title,descInput:a.state.groupData.groupInfo.description,sizeInput:a.state.groupData.groupInfo.size,priceInput:a.state.groupData.groupInfo.cost,tagsInput:a.state.groupData.groupInfo.tags})},a.changeGroupData=function(){a.state.titleInput!==a.state.groupData.groupInfo.title&&a.props.editGroupTitle(a.state.id,a.state.titleInput),a.state.descInput!==a.state.groupData.groupInfo.description&&a.props.editGroupDescription(a.state.id,a.state.descInput),a.state.sizeInput!==a.state.groupData.groupInfo.size&&a.props.editGroupSize(a.state.id,a.state.sizeInput),a.state.priceInput!==a.state.groupData.groupInfo.cost&&a.props.editGroupPrice(a.state.id,a.state.priceInput),a.state.groupTypeInput!==Object(j.b)(a.state.groupData.groupInfo.groupType)&&a.props.editGroupType(a.state.id,a.state.groupTypeInput),a.state.privateInput!==a.state.groupData.groupInfo.isPrivate&&a.props.editPrivacy(a.state.id,a.state.privateInput),a.state.tagsInput.length===a.state.groupData.groupInfo.tags.length&&0===a.state.tagsInput.filter(function(e,t){return e!==a.state.groupData.groupInfo.tags[t]}).length||a.props.editGroupTags(a.state.id,a.state.tagsInput),a.setState({isEditing:!1}),a.setState({needUpdate:!0})},a.state={id:a.props.match.params.id,groupData:{groupInfo:{isPrivate:!0,title:"",description:"",isActive:!0,tags:[],cost:null,size:0,groupType:"",memberAmount:0},members:[]},userData:localStorage.getItem("token")?Object(j.d)(localStorage.getItem("token")):null,isInGroup:!1,isCreator:!1,isTeacher:!1,needUpdate:!1,signInVisible:!1,isEditing:!1,titleInput:"",descInput:"",tagsInput:[],sizeInput:"",priceInput:"",groupTypeInput:"",privateInput:null},a.onSetResult=a.onSetResult.bind(a),a.getCurrentGroup=a.getCurrentGroup.bind(a),a.onSignInClick=a.onSignInClick.bind(a),a.handleCancel=a.handleCancel.bind(a),a.changeGroupData=a.changeGroupData.bind(a),a.onChangeTitleHandle=a.onChangeTitleHandle.bind(a),a.onChangeDescriptionHandle=a.onChangeDescriptionHandle.bind(a),a.onChangePriceHandle=a.onChangePriceHandle.bind(a),a.onChangeSizeHandle=a.onChangeSizeHandle.bind(a),a.onChangeTagsHandle=a.onChangeTagsHandle.bind(a),a.onHandleGroupTypeChange=a.onHandleGroupTypeChange.bind(a),a.onHandlePrivateChange=a.onHandlePrivateChange.bind(a),a.cancelChanges=a.cancelChanges.bind(a),a}return a(n,t),T(n,[{key:"componentDidMount",value:function(){this.getCurrentGroup()}},{key:"componentDidUpdate",value:function(e,t){t.needUpdate!==this.state.needUpdate&&(this.getCurrentGroup(),this.setState({needUpdate:!1}))}},{key:"onSetResult",value:function(e){var t=this;this.setState({groupData:{groupInfo:e.groupInfo,members:e.members},titleInput:e.groupInfo.title,descInput:e.groupInfo.description,sizeInput:e.groupInfo.size,priceInput:e.groupInfo.cost,tagsInput:e.groupInfo.tags,groupTypeInput:Object(j.b)(e.groupInfo.groupType),privateInput:e.groupInfo.isPrivate,isInGroup:!!this.state.userData&&Boolean(e.members.find(function(e){return e.userId===t.state.userData.UserId}))}),this.setState({isCreator:!!this.state.isInGroup&&Boolean(2===e.members.find(function(e){return e.userId===t.state.userData.UserId}).role),isTeacher:!!this.state.isInGroup&&Boolean(3===e.members.find(function(e){return e.userId===t.state.userData.UserId}).role)})}},{key:"render",value:function(){var e=this;return E("div",{},void 0,E(y.Col,{span:20,offset:2,style:{marginTop:40,marginBottom:160,fontSize:16}},void 0,E(y.Col,{className:"md-center-container",xs:{span:24},md:{span:10},lg:{span:7}},void 0,E(y.Row,{className:"main-group-info"},void 0,E(y.Row,{style:{marginBottom:26}},void 0,E(y.Col,{span:24},void 0,E("h3",{style:{margin:0,fontSize:22}},void 0,this.state.isEditing?E(y.Input,{onChange:this.onChangeTitleHandle,value:this.state.titleInput}):this.state.groupData.groupInfo.title)),Boolean(this.state.groupData.members.find(function(e){return 3==e.role}))?E("span",{style:{color:"rgba(0,0,0,0.6)"}},void 0,"Преподаватель найден"):E("span",{style:{color:"rgba(0,0,0,0.6)"}},void 0,"Идет поиск преподавателя")),E(y.Row,{gutter:6,type:"flex",justify:"start",align:"middle",style:{marginBottom:8}},void 0,this.state.isEditing?E(y.Select,{onChange:this.onChangeTagsHandle,defaultActiveFirstOption:!1,value:this.state.tagsInput,mode:"tags",style:{width:"100%"}},void 0,x,P,D,A):this.state.groupData.groupInfo.tags.map(function(e){return E(I.Link,{to:"#"},e,e)})),E(y.Row,{type:"flex",justify:"space-between",align:"middle",style:{marginBottom:8}},void 0,G,E(y.Col,{},void 0,this.state.isEditing?E(y.Select,{onChange:this.onHandleGroupTypeChange,defaultActiveFirstOption:!1,value:this.state.groupTypeInput,style:{minWidth:114}},void 0,w,U,k):Object(j.b)(this.state.groupData.groupInfo.groupType))),E(y.Row,{type:"flex",justify:"space-between",align:"middle",style:{marginBottom:8}},void 0,B,E(y.Col,{},void 0,this.state.isEditing?E(y.InputNumber,{min:0,value:this.state.priceInput,onChange:this.onChangePriceHandle}):this.state.groupData.groupInfo.cost," руб.")),this.state.isEditing?E(y.Row,{type:"flex",align:"middle",style:{marginBottom:12}},void 0,z,E(y.Col,{span:8,style:{textAlign:"right"}},void 0,E(y.Switch,{value:this.state.privateInput,id:"privacy",onChange:this.onHandlePrivateChange}))):this.state.groupData.groupInfo.isPrivate?E(y.Row,{style:{marginBottom:12}},void 0,L):E(y.Row,{style:{marginBottom:12}},void 0,N),this.state.isEditing?E(y.Row,{type:"flex",align:"middle",style:{marginBottom:12}},void 0,H,E(y.Col,{span:14,style:{textAlign:"right"}},void 0,E(y.InputNumber,{min:0,id:"size",value:this.state.sizeInput,onChange:this.onChangeSizeHandle,style:{width:64}}))):null),E(y.Row,{style:{width:"100%",marginBottom:20}},void 0,E(S.a,{members:this.state.groupData.members,memberAmount:this.state.groupData.groupInfo.memberAmount,size:this.state.groupData.groupInfo.size,isCreator:this.state.isCreator})),this.state.isCreator?E(y.Row,{style:{width:"100%"},className:"md-center-container"},void 0,E(C.a,{groupId:this.state.id})):null,this.state.isCreator&&!this.state.isEditing?E(y.Row,{},void 0,E(y.Button,{type:"dashed",className:"md-center-container md-offset-16px",onClick:function(){return e.setState({isEditing:!0})},style:{width:280,marginTop:12}},void 0,"Редактировать")):this.state.isEditing?E(y.Row,{},void 0,E(y.Col,{span:24,className:"md-center-container md-offset-16px"},void 0,E(y.Button,{type:"primary",onClick:this.changeGroupData,style:{width:280,marginTop:22}},void 0,"Подтвердить")),E(y.Col,{span:24,className:"md-center-container md-offset-16px"},void 0,E(y.Button,{type:"danger",onClick:this.cancelChanges,style:{width:280,marginTop:10}},void 0,"Отмена"))):null),E(y.Col,{xs:{span:24},md:{span:12,offset:2},lg:{span:15,offset:2},xl:{span:16,offset:1}},void 0,E(y.Row,{style:{textAlign:"right",marginTop:8}},void 0,this.state.groupData.groupInfo.memberAmount<this.state.groupData.groupInfo.size?this.state.isInGroup?E(y.Row,{className:"md-center-container"},void 0,E(y.Button,{onClick:function(){e.setState({needUpdate:!0}),e.props.leaveGroup(e.state.id,e.state.userData.UserId,e.state.isTeacher?"Teacher":"Member")}},void 0,"Покинуть группу")):E(y.Row,{className:"md-center-container"},void 0,E(y.Button,{type:"primary",onClick:function(){e.state.userData?(e.props.enterGroup(e.state.id),e.setState({needUpdate:!0})):e.onSignInClick()}},void 0,"Вступить в группу")):null),E(y.Row,{},void 0,E(y.Row,{style:{marginTop:42}},void 0,E(y.Col,{},void 0,E("h3",{style:{fontSize:18}},void 0,"Описание"))),E(y.Row,{style:{marginBottom:40}},void 0,E("p",{},void 0,this.state.isEditing?E(y.Input.TextArea,{onChange:this.onChangeDescriptionHandle,defaultValue:this.state.descInput,autosize:!0}):this.state.groupData.groupInfo.description))),E(y.Row,{style:{width:"100%"}},void 0,E(O.a,{isInGroup:this.state.isInGroup})))),E(_.a,{visible:this.state.signInVisible,handleCancel:this.handleCancel}))}}]),n}(u.a.Component);M.defaultProps={users:"true"===localStorage.getItem("withoutServer")?["Первый пользователь","Второй пользователь"]:[]};var F=Object(d.b)({}),V=Object(p.b)(F,i),J=Object(f.a)({key:"groupPage",reducer:h.a}),Y=Object(g.a)({key:"groupPage",saga:m.a});t.default=Object(l.c)(J,Y,V)(M)}.call(t,n("./node_modules/exports-loader/index.js?self.fetch!./node_modules/whatwg-fetch/fetch.js"))},"./app/containers/GroupPage/reducer.js":function(e,t,n){"use strict";function r(){var e=arguments.length>0&&void 0!==arguments[0]?arguments[0]:s,t=arguments[1];switch(t.type){case a.w:return e.set("pending",!0);case a.x:return e.set("pending",!1);case a.v:return e.set("pending",!1).set("error",!0);case a.C:return e.set("pending",!0);case a.D:return e.set("pending",!1);case a.B:return e.set("pending",!1).set("error",!0);case a.z:return e.set("pending",!0);case a.A:return i.message.success("Приглашение отправлено"),e.set("pending",!1);case a.y:return e.set("pending",!1).set("error",!0);case a.p:return e.set("pending",!0);case a.r:return e.set("pending",!1);case a.q:return e.set("pending",!1).set("error",!0);case a.a:return e.set("pending",!0);case a.c:return e.set("pending",!1);case a.b:return e.set("pending",!1).set("error",!0);case a.m:return e.set("pending",!0);case a.o:return e.set("pending",!1);case a.n:return e.set("pending",!1).set("error",!0);case a.j:return e.set("pending",!0);case a.l:return e.set("pending",!1);case a.k:return e.set("pending",!1).set("error",!0);case a.d:return e.set("pending",!0);case a.f:return e.set("pending",!1);case a.e:return e.set("pending",!1).set("error",!0);case a.s:return e.set("pending",!0);case a.u:return e.set("pending",!1);case a.t:return e.set("pending",!1).set("error",!0);case a.g:return e.set("pending",!0);case a.i:return e.set("pending",!1);case a.h:return e.set("pending",!1).set("error",!0);case a.E:return e.set("pending",!0).set("username",t.username).set("groupId",t.groupId);case a.G:return e.set("pending",!1).set("users",t.payload);case a.F:return e.set("pending",!1).set("error",!0);default:return e}}var o=n("./node_modules/immutable/dist/immutable.js"),a=(n.n(o),n("./app/containers/GroupPage/constants.js")),i=n("./node_modules/antd/lib/index.js"),s=(n.n(i),Object(o.fromJS)({username:"",groupId:"",users:[],pending:!1,error:!1}));t.a=r},"./app/containers/GroupPage/saga.js":function(e,t,n){"use strict";(function(e){function r(e){return regeneratorRuntime.wrap(function(t){for(;;)switch(t.prev=t.next){case 0:return t.prev=0,t.next=3,Object(E.a)(s,e.groupId);case 3:return t.next=5,Object(E.b)(Object(T.x)());case 5:t.next=11;break;case 7:return t.prev=7,t.t0=t.catch(0),t.next=11,Object(E.b)(Object(T.w)(t.t0));case 11:case"end":return t.stop()}},P,this,[[0,7]])}function o(e){return regeneratorRuntime.wrap(function(t){for(;;)switch(t.prev=t.next){case 0:return t.prev=0,t.next=3,Object(E.a)(u,e.groupId,e.memberId,e.role);case 3:return t.next=5,Object(E.b)(Object(T.D)());case 5:t.next=11;break;case 7:return t.prev=7,t.t0=t.catch(0),t.next=11,Object(E.b)(Object(T.C)(t.t0));case 11:case"end":return t.stop()}},D,this,[[0,7]])}function a(e){return regeneratorRuntime.wrap(function(t){for(;;)switch(t.prev=t.next){case 0:return t.prev=0,t.next=3,Object(E.a)(i,e.groupId,e.invitedId,e.role);case 3:return t.next=5,Object(E.b)(Object(T.A)());case 5:t.next=11;break;case 7:return t.prev=7,t.t0=t.catch(0),t.next=11,Object(E.b)(Object(T.z)(t.t0));case 11:case"end":return t.stop()}},A,this,[[0,7]])}function i(t,n,r){return e(x.a.API_BASE_URL+"/group/"+t+"/member/invitation",{method:"POST",headers:{"Content-Type":"application/json-patch+json",Authorization:"Bearer "+localStorage.getItem("token")},body:JSON.stringify({invitedId:n,role:r})}).catch(function(e){return e})}function s(t){return e(x.a.API_BASE_URL+"/group/"+t+"/member",{method:"POST",headers:{"Content-Type":"application/json-patch+json",Authorization:"Bearer "+localStorage.getItem("token")}}).catch(function(e){return e})}function u(t,n,r){return"Member"===r?e(x.a.API_BASE_URL+"/group/"+t+"/member/"+n,{method:"DELETE",headers:{"Content-Type":"application/json-patch+json",Authorization:"Bearer "+localStorage.getItem("token")}}).catch(function(e){return e}):"Teacher"===r?e(x.a.API_BASE_URL+"/group/"+t+"/member/teacher/"+n,{method:"DELETE",headers:{"Content-Type":"application/json-patch+json",Authorization:"Bearer "+localStorage.getItem("token")}}).catch(function(e){return e}):void 0}function c(e){return regeneratorRuntime.wrap(function(t){for(;;)switch(t.prev=t.next){case 0:return t.prev=0,t.next=3,Object(E.a)(p,e.id,e.title);case 3:return t.next=5,Object(E.b)(Object(T.o)());case 5:t.next=11;break;case 7:return t.prev=7,t.t0=t.catch(0),t.next=11,Object(E.b)(Object(T.n)(t.t0));case 11:case"end":return t.stop()}},G,this,[[0,7]])}function p(t,n){return e(x.a.API_BASE_URL+"/group/"+t+"/title",{method:"PUT",headers:{"Content-Type":"application/json-patch+json",Authorization:"Bearer "+localStorage.getItem("token")},body:JSON.stringify({groupTitle:n})}).then(function(e){return e.json()}).catch(function(e){return e})}function d(e){return regeneratorRuntime.wrap(function(t){for(;;)switch(t.prev=t.next){case 0:return t.prev=0,t.next=3,Object(E.a)(l,e.id,e.groupType);case 3:return t.next=5,Object(E.b)(Object(T.r)());case 5:t.next=11;break;case 7:return t.prev=7,t.t0=t.catch(0),t.next=11,Object(E.b)(Object(T.q)(t.t0));case 11:case"end":return t.stop()}},w,this,[[0,7]])}function l(t,n){return e(x.a.API_BASE_URL+"/group/"+t+"/type",{method:"PUT",headers:{"Content-Type":"application/json-patch+json",Authorization:"Bearer "+localStorage.getItem("token")},body:JSON.stringify({groupType:n})}).then(function(e){return e.json()}).catch(function(e){return e})}function g(e){return regeneratorRuntime.wrap(function(t){for(;;)switch(t.prev=t.next){case 0:return t.prev=0,t.next=3,Object(E.a)(f,e.id,e.isPrivate);case 3:return t.next=5,Object(E.b)(Object(T.u)());case 5:t.next=11;break;case 7:return t.prev=7,t.t0=t.catch(0),t.next=11,Object(E.b)(Object(T.t)(t.t0));case 11:case"end":return t.stop()}},U,this,[[0,7]])}function f(t,n){return e(x.a.API_BASE_URL+"/group/"+t+"/privacy",{method:"PUT",headers:{"Content-Type":"application/json-patch+json",Authorization:"Bearer "+localStorage.getItem("token")},body:JSON.stringify({isPrivate:n})}).then(function(e){return e.json()}).catch(function(e){return e})}function h(e){return regeneratorRuntime.wrap(function(t){for(;;)switch(t.prev=t.next){case 0:return t.prev=0,t.next=3,Object(E.a)(m,e.id,e.description);case 3:return t.next=5,Object(E.b)(Object(T.c)());case 5:t.next=11;break;case 7:return t.prev=7,t.t0=t.catch(0),t.next=11,Object(E.b)(Object(T.b)(t.t0));case 11:case"end":return t.stop()}},k,this,[[0,7]])}function m(t,n){return e(x.a.API_BASE_URL+"/group/"+t+"/description",{method:"PUT",headers:{"Content-Type":"application/json-patch+json",Authorization:"Bearer "+localStorage.getItem("token")},body:JSON.stringify({groupDescription:n})}).then(function(e){return e.json()}).catch(function(e){return e})}function b(e){return regeneratorRuntime.wrap(function(t){for(;;)switch(t.prev=t.next){case 0:return t.prev=0,t.next=3,Object(E.a)(I,e.id,e.tags);case 3:return t.next=5,Object(E.b)(Object(T.l)());case 5:t.next=11;break;case 7:return t.prev=7,t.t0=t.catch(0),t.next=11,Object(E.b)(Object(T.k)(t.t0));case 11:case"end":return t.stop()}},B,this,[[0,7]])}function I(t,n){return e(x.a.API_BASE_URL+"/group/"+t+"/tags",{method:"PUT",headers:{"Content-Type":"application/json-patch+json",Authorization:"Bearer "+localStorage.getItem("token")},body:JSON.stringify({groupTags:n})}).then(function(e){return e.json()}).catch(function(e){return e})}function v(e){return regeneratorRuntime.wrap(function(t){for(;;)switch(t.prev=t.next){case 0:return t.prev=0,t.next=3,Object(E.a)(j,e.id,e.size);case 3:return t.next=5,Object(E.b)(Object(T.i)());case 5:t.next=11;break;case 7:return t.prev=7,t.t0=t.catch(0),t.next=11,Object(E.b)(Object(T.h)(t.t0));case 11:case"end":return t.stop()}},z,this,[[0,7]])}function j(t,n){return e(x.a.API_BASE_URL+"/group/"+t+"/size",{method:"PUT",headers:{"Content-Type":"application/json-patch+json",Authorization:"Bearer "+localStorage.getItem("token")},body:JSON.stringify({groupSize:n})}).then(function(e){return e.json()}).catch(function(e){return e})}function y(e){return regeneratorRuntime.wrap(function(t){for(;;)switch(t.prev=t.next){case 0:return t.prev=0,t.next=3,Object(E.a)(S,e.id,e.price);case 3:return t.next=5,Object(E.b)(Object(T.f)());case 5:t.next=11;break;case 7:return t.prev=7,t.t0=t.catch(0),t.next=11,Object(E.b)(Object(T.e)(t.t0));case 11:case"end":return t.stop()}},L,this,[[0,7]])}function S(t,n){return e(x.a.API_BASE_URL+"/group/"+t+"/price",{method:"PUT",headers:{"Content-Type":"application/json-patch+json",Authorization:"Bearer "+localStorage.getItem("token")},body:JSON.stringify({groupPrice:n})}).then(function(e){return e.json()}).catch(function(e){return e})}function O(e){var t;return regeneratorRuntime.wrap(function(n){for(;;)switch(n.prev=n.next){case 0:return n.prev=0,n.next=3,Object(E.a)(C,e.groupId,e.username);case 3:return t=n.sent,n.next=6,Object(E.b)(Object(T.G)(t.users));case 6:n.next=12;break;case 8:return n.prev=8,n.t0=n.catch(0),n.next=12,Object(E.b)(Object(T.F)(n.t0));case 12:case"end":return n.stop()}},N,this,[[0,8]])}function C(t,n){return e(x.a.API_BASE_URL+"/users/searchForInvitation",{method:"POST",headers:{"Content-Type":"application/json-patch+json"},body:JSON.stringify({groupId:t,username:n})}).then(function(e){return e.json()}).then(function(e){return e}).catch(function(e){return e})}function _(){return regeneratorRuntime.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:return e.next=2,Object(E.c)(R.w,r);case 2:return e.next=4,Object(E.c)(R.C,o);case 4:return e.next=6,Object(E.c)(R.z,a);case 6:return e.next=8,Object(E.c)(R.p,c);case 8:return e.next=10,Object(E.c)(R.a,h);case 10:return e.next=12,Object(E.c)(R.m,b);case 12:return e.next=14,Object(E.c)(R.j,v);case 14:return e.next=16,Object(E.c)(R.d,y);case 16:return e.next=18,Object(E.c)(R.g,g);case 18:return e.next=20,Object(E.c)(R.s,d);case 20:return e.next=22,Object(E.c)(R.E,O);case 22:case"end":return e.stop()}},H,this)}t.a=_;var E=n("./node_modules/redux-saga/es/effects.js"),T=n("./app/containers/GroupPage/actions.js"),R=n("./app/containers/GroupPage/constants.js"),x=n("./app/config.js"),P=regeneratorRuntime.mark(r),D=regeneratorRuntime.mark(o),A=regeneratorRuntime.mark(a),G=regeneratorRuntime.mark(c),w=regeneratorRuntime.mark(d),U=regeneratorRuntime.mark(g),k=regeneratorRuntime.mark(h),B=regeneratorRuntime.mark(b),z=regeneratorRuntime.mark(v),L=regeneratorRuntime.mark(y),N=regeneratorRuntime.mark(O),H=regeneratorRuntime.mark(_)}).call(t,n("./node_modules/exports-loader/index.js?self.fetch!./node_modules/whatwg-fetch/fetch.js"))}});