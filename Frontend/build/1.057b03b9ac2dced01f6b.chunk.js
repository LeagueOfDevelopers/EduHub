webpackJsonp([1],{"./app/containers/ProfilePage/actions.js":function(e,t,n){"use strict";function r(e){return{type:j.p,id:e}}function o(e){return{type:j.r,groups:e}}function a(e){return{type:j.q,error:e}}function i(e){return{type:j.m,username:e}}function s(){return{type:j.o}}function c(e){return{type:j.n,error:e}}function u(e){return{type:j.a,aboutUser:e}}function d(){return{type:j.c}}function p(e){return{type:j.b,error:e}}function l(e){return{type:j.d,birthYear:e}}function f(){return{type:j.f}}function h(e){return{type:j.e,error:e}}function g(){return{type:j.l}}function m(e){return{type:j.k,error:e}}function b(e){return{type:j.g,contacts:e}}function v(){return{type:j.i}}function y(e){return{type:j.h,error:e}}t.o=r,t.q=o,t.p=a,t.l=i,t.n=s,t.m=c,t.a=u,t.c=d,t.b=p,t.d=l,t.f=f,t.e=h,t.k=g,t.j=m,t.g=b,t.i=v,t.h=y;var j=n("./app/containers/ProfilePage/constants.js")},"./app/containers/ProfilePage/constants.js":function(e,t,n){"use strict";n.d(t,"p",function(){return r}),n.d(t,"r",function(){return o}),n.d(t,"q",function(){return a}),n.d(t,"m",function(){return i}),n.d(t,"o",function(){return s}),n.d(t,"n",function(){return c}),n.d(t,"a",function(){return u}),n.d(t,"c",function(){return d}),n.d(t,"b",function(){return p}),n.d(t,"j",function(){return l}),n.d(t,"l",function(){return f}),n.d(t,"k",function(){return h}),n.d(t,"d",function(){return g}),n.d(t,"f",function(){return m}),n.d(t,"e",function(){return b}),n.d(t,"g",function(){return v}),n.d(t,"i",function(){return y}),n.d(t,"h",function(){return j});var r="GET_CURRENT_USER_GROUPS",o="GET_CURRENT_USER_GROUPS_SUCCESS",a="GET_CURRENT_USER_GROUPS_FAILED",i="EDIT_NAME",s="EDIT_NAME_SUCCESS",c="EDIT_NAME_FAILED",u="EDIT_ABOUT_USER_INFO",d="EDIT_ABOUT_USER_INFO_SUCCESS",p="EDIT_ABOUT_USER_INFO_FAILED",l="EDIT_GENDER",f="EDIT_GENDER_SUCCESS",h="EDIT_GENDER_FAILED",g="EDIT_BIRTH_YEAR",m="EDIT_BIRTH_YEAR_SUCCESS",b="EDIT_BIRTH_YEAR_FAILED",v="EDIT_CONTACTS",y="EDIT_CONTACTS_SUCCESS",j="EDIT_CONTACTS_FAILED"},"./app/containers/ProfilePage/index.js":function(e,t,n){"use strict";Object.defineProperty(t,"__esModule",{value:!0}),function(e){function r(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}function o(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}function a(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}function i(e){return{getCurrentUserGroups:function(t){return e(Object(m.o)(t))},editUsername:function(t){return e(Object(m.l)(t))},editAboutUser:function(t){return e(Object(m.a)(t))},editBirthYear:function(t){return e(Object(m.d)(t))},editContacts:function(t){return e(Object(m.g)(t))}}}n.d(t,"ProfilePage",function(){return Y});var s=n("./node_modules/react/react.js"),c=n.n(s),u=n("./node_modules/prop-types/index.js"),d=(n.n(u),n("./node_modules/react-redux/es/index.js")),p=n("./node_modules/reselect/es/index.js"),l=n("./node_modules/redux/es/index.js"),f=n("./app/utils/injectSaga.js"),h=n("./app/utils/injectReducer.js"),g=n("./app/containers/ProfilePage/selectors.js"),m=n("./app/containers/ProfilePage/actions.js"),b=n("./app/containers/ProfilePage/reducer.js"),v=n("./app/containers/ProfilePage/saga.js"),y=n("./app/globalJS.js"),j=n("./app/config.js"),I=n("./node_modules/react-router-dom/index.js"),P=(n.n(I),n("./app/components/UnassembledGroupCard/index.js")),S=n("./node_modules/antd/lib/index.js"),C=(n.n(S),function(){var e="function"==typeof Symbol&&Symbol.for&&Symbol.for("react.element")||60103;return function(t,n,r,o){var a=t&&t.defaultProps,i=arguments.length-3;if(n||0===i||(n={}),n&&a)for(var s in a)void 0===n[s]&&(n[s]=a[s]);else n||(n=a||{});if(1===i)n.children=o;else if(i>1){for(var c=Array(i),u=0;u<i;u++)c[u]=arguments[u+3];n.children=c}return{$$typeof:e,type:t,key:void 0===r?null:""+r,ref:null,props:n,_owner:null}}}()),_=function(){function e(e,t){for(var n=0;n<t.length;n++){var r=t[n];r.enumerable=r.enumerable||!1,r.configurable=!0,"value"in r&&(r.writable=!0),Object.defineProperty(e,r.key,r)}}return function(t,n,r){return n&&e(t.prototype,n),r&&e(t,r),t}}(),O=S.Tabs.TabPane,E={userProfile:{name:"Имя пользователя",tags:["js","c#"],sex:"Мужской",years:19,experience:3,description:"Краткая инфа о себе. Краткая инфа о себе. Краткая инфа о себе.\n                  Краткая инфа о себе.",links:["LinkedIn","Vk"]}},x=[{groupInfo:{id:1,title:"cdcvvdsc",length:6,size:8,moneyPerUser:600,groupType:"Lfdsv",tags:["fds","sdf"]}},{groupInfo:{id:3,title:"dscsdc",length:6,size:8,moneyPerUser:600,groupType:"Lfdsv",tags:["fds","sdf"]}},{groupInfo:{id:1,title:"cdcvvdsc",length:6,size:8,moneyPerUser:600,groupType:"Lfdsv",tags:["fds","sdf"]}},{groupInfo:{id:3,title:"dscsdc",length:6,size:8,moneyPerUser:600,groupType:"Lfdsv",tags:["fds","sdf"]}}],U=C("div",{},void 0,"Почтовый адрес"),T=C("div",{},void 0,"Пол"),R=C(S.Select.Option,{value:"true"},void 0,"Мужской"),k=C(S.Select.Option,{value:"false"},void 0,"Женский"),w=C("div",{},void 0,"Год рождения"),A=C("div",{},void 0,"Основные навыки"),B=C("div",{},void 0,"О себе"),N=C("div",{},void 0,"Ссылки"),D=C(S.Icon,{type:"plus"}),L=C(O,{tab:"Профиль преподавателя"},"3"),Y=function(t){function i(t){r(this,i);var n=o(this,(i.__proto__||Object.getPrototypeOf(i)).call(this,t));return n.getCurrentUser=function(t){e(j.a.API_BASE_URL+"/user/profile/"+t,{headers:{"Content-Type":"application/json-patch+json"}}).then(function(e){return e.json()}).then(function(e){return n.onSetResult(e)}).catch(function(e){return e})},n.onSetResult=function(e){n.setState({userProfile:e.userProfile?e.userProfile:{},teacherProfile:e.teacherProfile,nameInput:e.userProfile.name,sexInput:e.userProfile.isMan.toString(),birthYearInput:e.userProfile.birthYear,aboutInput:e.userProfile.aboutUser,contactsInputs:e.userProfile.contacts?e.userProfile.contacts:[]})},n.onChangeNameHandle=function(e){n.setState({nameInput:e.target.value})},n.onChangeSexHandle=function(e){n.setState({sexInput:e})},n.onChangebirthYearHandle=function(e){n.setState({birthYearInput:e})},n.onChangeAboutHandle=function(e){n.setState({aboutInput:e.target.value})},n.addContact=function(){n.setState({contactsInputs:n.state.contactsInputs.concat("")})},n.removeContact=function(e){n.setState({contactsInputs:n.state.contactsInputs.filter(function(t,n){return n!==e})})},n.onHandleChangeContact=function(e,t){n.setState({contactsInputs:n.state.contactsInputs.map(function(n,r){return r===t?e.target.value:n})})},n.cancelChanges=function(){n.setState({isEditing:!1,nameInput:n.state.userProfile.name,aboutInput:n.state.userProfile.aboutUser,birthYearInput:n.state.userProfile.birthYear,contactsInputs:n.state.userProfile.contacts?n.state.userProfile.contacts:[]})},n.changeProfileData=function(){n.setState({contactsInputs:n.state.contactsInputs.filter(function(e){return""!==e})}),n.state.nameInput!==n.state.userProfile.name&&(n.props.editUsername(n.state.nameInput),localStorage.setItem("name",""+n.state.nameInput)),n.state.aboutInput!==n.state.userProfile.aboutUser&&n.props.editAboutUser(n.state.aboutInput),n.state.birthYearInput!==n.state.userProfile.birthYear&&n.props.editBirthYear(n.state.birthYearInput),n.state.contactsInputs.length===n.state.userProfile.contacts&&0===n.state.contactsInputs.filter(function(e,t){return e!==n.state.userProfile.contacts[t]}).length||setTimeout(function(){return n.props.editContacts(n.state.contactsInputs)},0),n.setState({isEditing:!1}),n.setState({needUpdate:!0})},n.state={id:n.props.match.params.id,userProfile:{name:"",email:"",avatarLink:"",isMan:"",birthYear:"",aboutUser:"",contacts:[]},teacherProfile:{reviews:[],skills:[]},isEditing:!1,nameInput:"",sexInput:"",birthYearInput:"",aboutInput:"",contactsInputs:[],needUpdate:!1},n.onSetResult=n.onSetResult.bind(n),n.getCurrentUser=n.getCurrentUser.bind(n),n.onChangeNameHandle=n.onChangeNameHandle.bind(n),n.onChangeSexHandle=n.onChangeSexHandle.bind(n),n.onChangebirthYearHandle=n.onChangebirthYearHandle.bind(n),n.onChangeAboutHandle=n.onChangeAboutHandle.bind(n),n.changeProfileData=n.changeProfileData.bind(n),n.addContact=n.addContact.bind(n),n.removeContact=n.removeContact.bind(n),n.cancelChanges=n.cancelChanges.bind(n),n}return a(i,t),_(i,[{key:"componentDidMount",value:function(){"true"!==localStorage.getItem("without_server")?(this.props.getCurrentUserGroups(this.state.id),this.getCurrentUser(this.state.id)):this.onSetResult(E)}},{key:"componentDidUpdate",value:function(e,t){t.needUpdate!==this.state.needUpdate&&(this.getCurrentUser(this.state.id),this.setState({needUpdate:!1}))}},{key:"render",value:function(){var e=this;return C("div",{},void 0,C(S.Col,{span:20,offset:2,style:{marginTop:40,marginBottom:40},className:"md-center-container"},void 0,C(S.Col,{xs:{span:24},md:{span:10},lg:{span:6},className:"lg-center-container-item"},void 0,C(S.Card,{title:C(S.Row,{type:"flex",align:"middle"},void 0,C(S.Col,{span:21,style:{display:"flex",alignItems:"center"}},void 0,C(S.Avatar,{src:this.state.userProfile.avatarLink,style:{minHeight:50,minWidth:50,marginRight:20,borderRadius:"50%"}},void 0),C("span",{},void 0,this.state.isEditing?C(S.Input,{style:{width:"86%"},onChange:this.onChangeNameHandle,value:this.state.nameInput}):this.state.userProfile.name)),localStorage.getItem("token")&&!this.state.isEditing&&Object(y.c)(localStorage.getItem("token")).UserId===this.state.id?C(S.Col,{span:3,style:{textAlign:"right"}},void 0,C("img",{src:n("./app/images/edit.svg"),onClick:function(){return e.setState({isEditing:!0})},style:{width:20,cursor:"pointer"}})):null),hoverable:!0,className:"profile-card font-size-20 without-border-bottom"},void 0,C(S.Row,{style:{marginBottom:20}},void 0,U,C("div",{style:{fontSize:16,color:"#000"}},void 0,this.state.userProfile.email)),C(S.Row,{style:{marginBottom:20}},void 0,T,C("div",{style:{fontSize:16,color:"#000"}},void 0,this.state.isEditing?C(S.Select,{onChange:this.onChangeSexHandle,value:this.state.sexInput},void 0,R,k):this.state.userProfile.isMan?"Мужской":"Женский")),C(S.Row,{style:{marginBottom:20}},void 0,w,C("div",{style:{fontSize:16,color:"#000"}},void 0,this.state.isEditing?C(S.InputNumber,{onChange:this.onChangebirthYearHandle,style:{width:150},value:this.state.birthYearInput}):this.state.userProfile.birthYear?this.state.userProfile.birthYear:"Не указано")),C(S.Row,{style:{marginBottom:20}},void 0,A,C(S.Row,{gutter:6},void 0,this.state.teacherProfile&&this.state.teacherProfile.skills&&0!==this.state.teacherProfile.skills.length?this.state.teacherProfile.skills.map(function(e){return C(I.Link,{to:"#"},e,e)}):C("div",{style:{fontSize:16,color:"#000"}},void 0,"Не указано"))),C(S.Row,{style:{marginBottom:20}},void 0,B,C("div",{style:{fontSize:16,color:"#000"}},void 0,this.state.isEditing?C(S.Input.TextArea,{onChange:this.onChangeAboutHandle,defaultValue:this.state.aboutInput,autosize:!0}):this.state.userProfile.aboutUser?this.state.userProfile.aboutUser:"Не указано")),C(S.Row,{style:{marginBottom:20}},void 0,N,C("div",{},void 0,this.state.userProfile.contacts&&0!==this.state.userProfile.contacts.length&&!this.state.isEditing?this.state.userProfile.contacts.map(function(e,t){return C(I.Link,{to:"#",className:"user-link",style:{fontSize:16,display:"block"}},t,e)}):this.state.isEditing?C("div",{},void 0,this.state.contactsInputs.map(function(t,n){return C("div",{},n,C(S.Col,{span:20},void 0,C(S.Input,{placeholder:"Ссылка на профиль",onChange:function(t){return e.onHandleChangeContact(t,n)},value:e.state.contactsInputs[n],style:{marginBottom:8,width:"100%"}})),C(S.Col,{span:4,style:{textAlign:"right"}},void 0,C(S.Icon,{className:"dynamic-delete-button",type:"minus-circle-o",onClick:function(){return e.removeContact(n)}})))}),C(S.Button,{type:"dashed",onClick:this.addContact,style:{width:"100%",marginTop:8}},void 0,D,"Добавить ссылку")):C("div",{style:{fontSize:16,color:"#000"}},void 0,"Не указано"))),this.state.isEditing?C("div",{},void 0,C(S.Col,{span:24},void 0,C(S.Button,{type:"primary",onClick:this.changeProfileData,style:{width:"100%"}},void 0,"Подтвердить")),C(S.Col,{span:24},void 0,C(S.Button,{type:"danger",onClick:this.cancelChanges,style:{marginTop:6,width:"100%"}},void 0,"Отмена"))):null),C(I.Link,{to:"/create_group"},void 0,C(S.Button,{type:"primary",size:"large",style:{width:"100%",marginTop:20,minWidth:280}},void 0,"Создать группу")),this.state.teacherProfile?C(S.Button,{style:{width:"100%",marginTop:12,minWidth:280}},void 0,"Стать учеником"):C(S.Button,{type:"primary",style:{width:"100%",marginTop:20,minWidth:280}},void 0,"Стать преподавателем")),C(S.Col,{xs:{span:24},md:{span:12,offset:2},lg:{span:15,offset:3},className:"lg-center-container-item xs-groups-tabs"},void 0,C(S.Tabs,{defaultActiveKey:"1",type:"card"},void 0,C(O,{tab:"Группы"},"1",C("div",{className:"cards-holder md-cards-holder-center",style:{margin:"30px 0"}},void 0,"true"===localStorage.getItem("withoutServer")?x.map(function(e,t){return C(I.Link,{to:"/group/"+e.groupInfo.id},e.groupInfo.id,c.a.createElement(P.a,e))}):this.props.myGroups.map(function(e,t){return C(I.Link,{to:"/group/"+e.groupInfo.id},e.groupInfo.id,c.a.createElement(P.a,e))}))),L))))}}]),i}(c.a.Component);Y.defaultProps={};var H=Object(p.b)({myGroups:Object(g.a)()}),z=Object(d.b)(H,i),G=Object(h.a)({key:"profilePage",reducer:b.a}),F=Object(f.a)({key:"profilePage",saga:v.a});t.default=Object(l.c)(G,F,z)(Y)}.call(t,n("./node_modules/exports-loader/index.js?self.fetch!./node_modules/whatwg-fetch/fetch.js"))},"./app/containers/ProfilePage/reducer.js":function(e,t,n){"use strict";function r(){var e=arguments.length>0&&void 0!==arguments[0]?arguments[0]:i,t=arguments[1];switch(t.type){case a.p:return e.set("pending",!0);case a.r:return e.set("pending",!1).set("groups",t.groups);case a.q:return e.set("pending",!1).set("error",!0);case a.m:return e.set("pending",!0);case a.o:return location.reload("/"),e.set("pending",!1);case a.n:return e.set("pending",!1).set("error",!0);case a.a:return e.set("pending",!0);case a.c:return e.set("pending",!1);case a.b:return e.set("pending",!1).set("error",!0);case a.d:return e.set("pending",!0);case a.f:return e.set("pending",!1);case a.e:return e.set("pending",!1).set("error",!0);case a.j:return e.set("pending",!0);case a.l:return e.set("pending",!1);case a.k:return e.set("pending",!1).set("error",!0);case a.g:return e.set("pending",!0);case a.i:return e.set("pending",!1);case a.h:return e.set("pending",!1).set("error",!0);default:return e}}var o=n("./node_modules/immutable/dist/immutable.js"),a=(n.n(o),n("./app/containers/ProfilePage/constants.js")),i=Object(o.fromJS)({groups:[],pending:!1,error:!1});t.a=r},"./app/containers/ProfilePage/saga.js":function(e,t,n){"use strict";(function(e){function r(e){var t;return regeneratorRuntime.wrap(function(n){for(;;)switch(n.prev=n.next){case 0:return n.prev=0,n.next=3,Object(m.a)(o,e.id);case 3:return t=n.sent,n.next=6,Object(m.b)(Object(v.q)(t.groups));case 6:n.next=12;break;case 8:return n.prev=8,n.t0=n.catch(0),n.next=12,Object(m.b)(Object(v.p)(n.t0));case 12:case"end":return n.stop()}},j,this,[[0,8]])}function o(t){return e(y.a.API_BASE_URL+"/user/profile/groups/"+t,{method:"GET",headers:{"Content-Type":"application/json-patch+json",Authorization:"Bearer "+localStorage.getItem("token")}}).then(function(e){return e.json()}).then(function(e){return e}).catch(function(e){return e})}function a(e){return regeneratorRuntime.wrap(function(t){for(;;)switch(t.prev=t.next){case 0:return t.prev=0,t.next=3,Object(m.a)(i,e.username);case 3:return t.next=5,Object(m.b)(Object(v.n)());case 5:t.next=11;break;case 7:return t.prev=7,t.t0=t.catch(0),t.next=11,Object(m.b)(Object(v.m)(t.t0));case 11:case"end":return t.stop()}},I,this,[[0,7]])}function i(t){return e(y.a.API_BASE_URL+"/user/profile/name",{method:"PUT",headers:{"Content-Type":"application/json-patch+json",Authorization:"Bearer "+localStorage.getItem("token")},body:JSON.stringify({userName:t})}).then(function(e){return e.json()}).catch(function(e){return e})}function s(e){return regeneratorRuntime.wrap(function(t){for(;;)switch(t.prev=t.next){case 0:return t.prev=0,t.next=3,Object(m.a)(c,e.aboutUser);case 3:return t.next=5,Object(m.b)(Object(v.c)());case 5:t.next=11;break;case 7:return t.prev=7,t.t0=t.catch(0),t.next=11,Object(m.b)(Object(v.b)(t.t0));case 11:case"end":return t.stop()}},P,this,[[0,7]])}function c(t){return e(y.a.API_BASE_URL+"/user/profile/about",{method:"PUT",headers:{"Content-Type":"application/json-patch+json",Authorization:"Bearer "+localStorage.getItem("token")},body:JSON.stringify({aboutUser:t})}).then(function(e){return e.json()}).catch(function(e){return e})}function u(e){return regeneratorRuntime.wrap(function(t){for(;;)switch(t.prev=t.next){case 0:return t.prev=0,t.next=3,Object(m.a)(d,e.gender);case 3:return t.next=5,Object(m.b)(Object(v.k)());case 5:t.next=11;break;case 7:return t.prev=7,t.t0=t.catch(0),t.next=11,Object(m.b)(Object(v.j)(t.t0));case 11:case"end":return t.stop()}},S,this,[[0,7]])}function d(t){return e(y.a.API_BASE_URL+"/user/profile/gender",{method:"PUT",headers:{"Content-Type":"application/json-patch+json",Authorization:"Bearer "+localStorage.getItem("token")},body:JSON.stringify({gender:t})}).then(function(e){return e.json()}).catch(function(e){return e})}function p(e){return regeneratorRuntime.wrap(function(t){for(;;)switch(t.prev=t.next){case 0:return t.prev=0,t.next=3,Object(m.a)(l,e.birthYear);case 3:return t.next=5,Object(m.b)(Object(v.f)());case 5:t.next=11;break;case 7:return t.prev=7,t.t0=t.catch(0),t.next=11,Object(m.b)(Object(v.e)(t.t0));case 11:case"end":return t.stop()}},C,this,[[0,7]])}function l(t){return e(y.a.API_BASE_URL+"/user/profile/birthyear",{method:"PUT",headers:{"Content-Type":"application/json-patch+json",Authorization:"Bearer "+localStorage.getItem("token")},body:JSON.stringify({birthYear:t})}).then(function(e){return e.json()}).catch(function(e){return e})}function f(e){return regeneratorRuntime.wrap(function(t){for(;;)switch(t.prev=t.next){case 0:return t.prev=0,t.next=3,Object(m.a)(h,e.contacts);case 3:return t.next=5,Object(m.b)(Object(v.i)());case 5:t.next=11;break;case 7:return t.prev=7,t.t0=t.catch(0),t.next=11,Object(m.b)(Object(v.h)(t.t0));case 11:case"end":return t.stop()}},_,this,[[0,7]])}function h(t){return e(y.a.API_BASE_URL+"/user/profile/contacts",{method:"PUT",headers:{"Content-Type":"application/json-patch+json",Authorization:"Bearer "+localStorage.getItem("token")},body:JSON.stringify({contacts:t})}).then(function(e){return e.json()}).catch(function(e){return e})}function g(){return regeneratorRuntime.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:return e.next=2,Object(m.c)(b.p,r);case 2:return e.next=4,Object(m.c)(b.m,a);case 4:return e.next=6,Object(m.c)(b.a,s);case 6:return e.next=8,Object(m.c)(b.j,u);case 8:return e.next=10,Object(m.c)(b.d,p);case 10:return e.next=12,Object(m.c)(b.g,f);case 12:case"end":return e.stop()}},O,this)}t.a=g;var m=n("./node_modules/redux-saga/es/effects.js"),b=n("./app/containers/ProfilePage/constants.js"),v=n("./app/containers/ProfilePage/actions.js"),y=n("./app/config.js"),j=regeneratorRuntime.mark(r),I=regeneratorRuntime.mark(a),P=regeneratorRuntime.mark(s),S=regeneratorRuntime.mark(u),C=regeneratorRuntime.mark(p),_=regeneratorRuntime.mark(f),O=regeneratorRuntime.mark(g)}).call(t,n("./node_modules/exports-loader/index.js?self.fetch!./node_modules/whatwg-fetch/fetch.js"))},"./app/containers/ProfilePage/selectors.js":function(e,t,n){"use strict";n.d(t,"a",function(){return a});var r=n("./node_modules/reselect/es/index.js"),o=function(e){return e.get("profilePage")},a=function(){return Object(r.a)(o,function(e){return e.get("groups")})}},"./app/images/edit.svg":function(e,t,n){e.exports=n.p+"33ae15415ea9f00b0e605241ed6ec7ad.svg"}});