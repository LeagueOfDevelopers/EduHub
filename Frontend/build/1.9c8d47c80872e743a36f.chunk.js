webpackJsonp([1],{"./app/containers/Header/actions.js":function(e,t,n){"use strict";function o(e){return{type:c.e,name:e}}function a(e){return{type:c.f,payload:e||[]}}function i(e){return{type:c.d,payload:e}}function s(e){return{type:c.b,title:e}}function r(e){return{type:c.c,payload:e||[]}}function l(e){return{type:c.a,payload:e}}t.d=o,t.f=a,t.e=i,t.a=s,t.c=r,t.b=l;var c=n("./app/containers/Header/constants.js")},"./app/containers/Header/constants.js":function(e,t,n){"use strict";n.d(t,"e",function(){return o}),n.d(t,"f",function(){return a}),n.d(t,"d",function(){return i}),n.d(t,"b",function(){return s}),n.d(t,"c",function(){return r}),n.d(t,"a",function(){return l});var o="GET_USERS_START",a="GET_USERS_SUCCESS",i="GET_USERS_FAILED",s="GET_GROUPS_START",r="GET_GROUPS_SUCCESS",l="GET_GROUPS_FAILED"},"./app/containers/Header/index.js":function(e,t,n){"use strict";function o(){var e=arguments.length>0&&void 0!==arguments[0]?arguments[0]:b,t=arguments[1];switch(t.type){case y.e:return e.set("pending",!0).set("name",t.name);case y.f:return e.set("pending",!1).set("users",t.payload);case y.d:return e.set("pending",!1).set("error",!0);case y.b:return e.set("pending",!0).set("name",t.title);case y.c:return e.set("pending",!1).set("groups",t.payload);case y.a:return e.set("pending",!1).set("error",!0);default:return e}}function a(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}function i(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}function s(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}function r(e){return{getUsers:function(t){return e(Object(h.d)(t))},getGroups:function(t){return e(Object(h.a)(t))}}}Object.defineProperty(t,"__esModule",{value:!0});var l=n("./node_modules/react/react.js"),c=n.n(l),u=(n("./node_modules/prop-types/index.js"),n("./node_modules/react-redux/es/index.js")),d=n("./node_modules/reselect/es/index.js"),p=n("./node_modules/redux/es/index.js"),g=n("./app/utils/injectSaga.js"),m=n("./app/utils/injectReducer.js"),f=function(e){return e.get("header")},h=n("./app/containers/Header/actions.js"),v=n("./node_modules/immutable/dist/immutable.js"),y=n("./app/containers/Header/constants.js"),b=Object(v.fromJS)({name:"",users:[],groups:[],pending:!1,error:!1}),S=o,k=n("./app/containers/Header/saga.js"),I=n("./node_modules/styled-components/dist/styled-components.es.js"),j=n("./app/globalJS.js"),C=n("./node_modules/react-router-dom/index.js"),x=n("./app/containers/SigningInForm/index.js"),w=n("./node_modules/antd/lib/index.js"),_=n("./app/config.js"),L=function(){var e="function"==typeof Symbol&&Symbol.for&&Symbol.for("react.element")||60103;return function(t,n,o,a){var i=t&&t.defaultProps,s=arguments.length-3;if(n||0===s||(n={}),n&&i)for(var r in i)void 0===n[r]&&(n[r]=i[r]);else n||(n=i||{});if(1===s)n.children=a;else if(s>1){for(var l=Array(s),c=0;c<s;c++)l[c]=arguments[c+3];n.children=l}return{$$typeof:e,type:t,key:void 0===o?null:""+o,ref:null,props:n,_owner:null}}}(),O=function(){function e(e,t){for(var n=0;n<t.length;n++){var o=t[n];o.enumerable=o.enumerable||!1,o.configurable=!0,"value"in o&&(o.writable=!0),Object.defineProperty(e,o.key,o)}}return function(t,n,o){return n&&e(t.prototype,n),o&&e(t,o),t}}(),N=w.Select.Option,R=w.Select.OptGroup,A=I.a.div.withConfig({displayName:"Header__Logo"})(["font-size: 36px;"]),E=L("div",{},void 0,"Мой аккаунт"),T=L("div",{},void 0,"Уведомления"),U=L(w.Menu.Item,{className:"unhover"},"1",L(C.Link,{className:"profile",to:"/registration"},void 0,L(w.Button,{type:"primary",htmlType:"button"},void 0,"Зарегистрироваться"))),V=L(w.Col,{span:12},void 0,"Пользователи"),P=L(w.Col,{span:12},void 0,"Группы"),B=L(w.Col,{span:12},void 0,"Пользователи"),M=L(w.Col,{span:12},void 0,"Группы"),z=L(C.Link,{className:"profile",to:"/registration"},void 0,L(w.Button,{type:"primary",htmlType:"submit"},void 0,"Зарегистрироваться")),H=function(e){function t(e){a(this,t);var n=i(this,(t.__proto__||Object.getPrototypeOf(t)).call(this,e));return n.onSignInClick=function(){n.setState({signInVisible:!0})},n.handleCancel=function(){n.setState({signInVisible:!1})},n.logout=function(){localStorage.setItem("name",""),localStorage.setItem("avatarLink",""),localStorage.setItem("token",""),location.assign("/")},n.acc_menu=L(w.Menu,{},void 0,L(w.Menu.Item,{className:"menu-item"},"0",localStorage.getItem("token")?L(C.Link,{to:"/profile/"+Object(j.k)(localStorage.getItem("token")).UserId},void 0,E):null),L(w.Menu.Item,{className:"menu-item"},"1",localStorage.getItem("token")?L(C.Link,{to:"/profile/"+Object(j.k)(localStorage.getItem("token")).UserId+"/notifications"},void 0,T):null),L(w.Menu.Item,{className:"danger-menu-item menu-item"},"2",L("div",{style:{color:"red"},onClick:n.logout},void 0,"Выйти"))),n.menu=L(w.Menu,{},void 0,L(w.Menu.Item,{className:"unhover"},"0",L(w.Button,{className:"profile",style:{width:"100%"},htmlType:"button",onClick:n.onSignInClick},void 0,"Войти")),U),n.handleSelectChange=function(e){n.setState({searchValue:e}),n.props.getUsers(e),n.props.getGroups(e)},n.showSideMenu=function(){document.getElementById("side-menu").classList.remove("side-menu-hidden"),document.getElementById("side-menu").classList.add("side-menu"),setTimeout(function(){return document.body.style.overflowY="hidden"},300)},n.hideSideMenu=function(){document.getElementById("side-menu").classList.remove("side-menu"),document.getElementById("side-menu").classList.add("side-menu-hidden"),setTimeout(function(){return document.body.style.overflowY="scroll"},300)},n.state={signInVisible:!1,searchValue:""},n.logout=n.logout.bind(n),n.onSignInClick=n.onSignInClick.bind(n),n.handleCancel=n.handleCancel.bind(n),n.handleSelectChange=n.handleSelectChange.bind(n),n.showSideMenu=n.showSideMenu.bind(n),n.hideSideMenu=n.hideSideMenu.bind(n),n}return s(t,e),O(t,[{key:"render",value:function(){return L(w.Row,{type:"flex",align:"middle",className:"header",style:{width:"100hr"}},void 0,L(w.Col,{xs:{span:8,offset:2},md:{span:4,offset:2}},void 0,L(A,{},void 0,L(C.Link,{to:"/",style:{color:"#ffffff"}},void 0,"EduHub"))),L(w.Col,{md:{span:7,offset:1},lg:{span:7,offset:0},style:{position:"relative"}},void 0,L(w.Select,{mode:"combobox",className:"search",style:{width:"100%"},value:this.state.searchValue,placeholder:"Поиск",size:"large",defaultActiveFirstOption:!1,showArrow:!1,onChange:this.handleSelectChange,notFoundContent:"Ничего не найдено",onSelect:function(e){return e.preventDefault()}},void 0,L(R,{label:L(w.Col,{},void 0,V,L(w.Col,{span:12,style:{textAlign:"right"}},void 0,L(C.Link,{to:"/users"+(this.state.searchValue?"?name="+this.state.searchValue:"")},void 0,"Расширенный поиск")))},1,this.state.searchValue?this.props.users.map(function(e,t){return t<3?L(N,{className:"search-option-item"},e.name+e.id+"user",L(C.Link,{className:"search-user-link",to:"/profile/"+e.id,style:{display:"flex",alignItems:"center"}},void 0,L(w.Avatar,{src:e.avatarLink?_.a.API_BASE_URL+"/file/img/"+e.avatarLink:null,size:"large",style:{marginRight:10}}),L(w.Col,{},void 0,L("div",{},void 0,e.name)))):""}):""),L(R,{label:L(w.Col,{},void 0,P,L(w.Col,{span:12,style:{textAlign:"right"}},void 0,L(C.Link,{to:"/groups"+(this.state.searchValue?"?name="+this.state.searchValue:"")},void 0,"Расширенный поиск")))},2,this.state.searchValue?this.props.groups.map(function(e,t){return t<3?L(N,{className:"search-option-item"},e.groupInfo.title+e.groupInfo.id+"group",L(C.Link,{className:"search-user-link",to:"/group/"+e.groupInfo.id},void 0,L("div",{},void 0,e.groupInfo.title),L("div",{},void 0,e.groupInfo.tags.map(function(e){return L(C.Link,{to:"/groups?tags="+e,style:{marginRight:6}},e,e)})))):""}):"")),L(w.Icon,{type:"search",className:"search",style:{fontSize:20,position:"absolute",top:10,right:10,opacity:.8}})),L(w.Col,{className:"menu-btn",xs:{span:12},md:{span:6,offset:2},lg:{span:9,offset:0}},void 0,L("img",{onClick:this.showSideMenu,style:{width:26,height:26,cursor:"pointer"},src:n("./app/images/menu-white.svg"),alt:""})),L(w.Col,{id:"side-menu",className:"side-menu-hidden"},void 0,L(w.Row,{type:"flex",style:{alignItems:"center",overflow:"hidden"}},void 0,L(w.Col,{xs:{span:2}},void 0,L("img",{onClick:this.hideSideMenu,style:{width:26,height:26,cursor:"pointer"},src:n("./app/images/right-arrow.svg"),alt:""})),L(w.Col,{className:"side-search",xs:{span:24}},void 0,L(w.Select,{mode:"combobox",style:{width:"100%"},value:this.state.searchValue,placeholder:"Поиск",size:"large",defaultActiveFirstOption:!1,showArrow:!1,onChange:this.handleSelectChange,notFoundContent:"Ничего не найдено",onSelect:function(e){return e.preventDefault()}},void 0,L(R,{label:L(w.Col,{},void 0,B,L(w.Col,{span:12,style:{textAlign:"right"}},void 0,L(C.Link,{to:"/users"+(this.state.searchValue?"?name="+this.state.searchValue:"")},void 0,"Расширенный поиск")))},1,this.state.searchValue?this.props.users.map(function(e,t){return t<3?L(N,{className:"search-option-item"},e.name+e.id+"user",L(C.Link,{className:"search-user-link",to:"/profile/"+e.id,style:{display:"flex",alignItems:"center"}},void 0,L(w.Avatar,{src:e.avatarLink?_.a.API_BASE_URL+"/file/img/"+e.avatarLink:null,size:"large",style:{marginRight:10}}),L(w.Col,{},void 0,L("div",{},void 0,e.name)))):""}):""),L(R,{label:L(w.Col,{},void 0,M,L(w.Col,{span:12,style:{textAlign:"right"}},void 0,L(C.Link,{to:"/groups"+(this.state.searchValue?"?name="+this.state.searchValue:"")},void 0,"Расширенный поиск")))},2,this.state.searchValue?this.props.groups.map(function(e,t){return t<3?L(N,{className:"search-option-item"},e.groupInfo.title+e.groupInfo.id+"group",L(C.Link,{className:"search-user-link",to:"/group/"+e.groupInfo.id},void 0,L("div",{},void 0,e.groupInfo.title),L("div",{},void 0,e.groupInfo.tags.map(function(e){return L(C.Link,{to:"/groups?tags="+e,style:{marginRight:6}},e,e)})))):""}):"")),L(w.Icon,{type:"search",style:{fontSize:20,position:"absolute",top:10,right:10,opacity:.8}})),localStorage.getItem("token")?L(w.Col,{span:24},void 0,L(w.Col,{span:24,style:{display:"flex",alignItems:"center",marginTop:22,marginBottom:18}},void 0,L(C.Link,{style:{color:"black"},to:"/profile/"+Object(j.k)(localStorage.getItem("token")).UserId},void 0,L("img",{src:""!==localStorage.getItem("avatarLink")&&"null"!==localStorage.getItem("avatarLink")?_.a.API_BASE_URL+"/file/img/"+localStorage.getItem("avatarLink"):null,style:{color:"rgba(0,0,0,0.65)",height:60,width:60,borderRadius:"50%",cursor:"pointer"}})),L(C.Link,{style:{color:"black",width:"calc(100% - 60px)"},to:"/profile/"+Object(j.k)(localStorage.getItem("token")).UserId},void 0,L("div",{className:"userName ellipsis",style:{cursor:"pointer",opacity:.8,fontSize:22}},void 0,localStorage.getItem("name")))),L(w.Col,{span:24,className:"menu-item"},void 0,localStorage.getItem("token")?L(C.Link,{style:{color:"black"},to:"/profile/"+Object(j.k)(localStorage.getItem("token")).UserId+"/notifications"},void 0,L("div",{style:{opacity:.8,display:"flex",alignItems:"center",fontSize:20}},void 0,L("img",{src:n("./app/images/notification.svg"),style:{width:28,marginRight:16,marginLeft:-1}}),"Уведомления")):null),L(w.Col,{span:24,className:"menu-item",style:{marginTop:12}},void 0,L("div",{style:{color:"red",opacity:.8,display:"flex",alignItems:"center",fontSize:20},onClick:this.logout},void 0,L("img",{src:n("./app/images/logout.svg"),style:{width:28,marginRight:12,marginLeft:2}}),"Выйти"))):L(w.Col,{span:24},void 0,L(w.Button,{htmlType:"button",size:"large",onClick:this.onSignInClick,style:{width:"100%",marginTop:20}},void 0,"Войти"),L(C.Link,{to:"/registration"},void 0,L(w.Button,{size:"large",type:"primary",style:{width:"100%",marginTop:12},htmlType:"submit"},void 0,"Зарегистрироваться"))))),localStorage.getItem("token")?L(w.Col,{className:"registered-person",xs:{span:12},md:{span:6,offset:2},lg:{span:9,offset:0}},void 0,L(w.Dropdown,{overlay:this.acc_menu,trigger:["click"]},void 0,L("div",{style:{display:"flex",justifyContent:"flex-end",alignItems:"center",marginLeft:"36%"}},void 0,L(w.Avatar,{src:""!==localStorage.getItem("avatarLink")&&"null"!==localStorage.getItem("avatarLink")?_.a.API_BASE_URL+"/file/img/"+localStorage.getItem("avatarLink"):null,size:"large",style:{color:"rgba(0,0,0,0.65)",minHeight:40,minWidth:40,cursor:"pointer"}}),L("span",{className:"userName",style:{whiteSpace:"nowrap",cursor:"pointer",color:"#ffffff"}},void 0,localStorage.getItem("name"))))):L(w.Col,{className:"unregistered-person",xs:{span:12},md:{span:6,offset:2},lg:{span:9,offset:0}},void 0,L(w.Button,{className:"profile",htmlType:"button",onClick:this.onSignInClick,style:{marginRight:"4%"}},void 0,"Войти"),L(x.a,{visible:this.state.signInVisible,handleCancel:this.handleCancel}),z))}}]),t}(c.a.PureComponent);H.defaultProps={users:"true"===localStorage.getItem("withoutServer")?["Первый пользователь","Второй пользователь","Третий пользователь","Четвертый пользователь","Пятый пользователь"]:[],groups:"true"===localStorage.getItem("withoutServer")?[{title:"Первая группа",tags:["js","c#"]},{title:"Вторая группа",tags:["js","react"]},{title:"Третья группа",tags:["c#",".Net"]},{title:"Четвертая группа",tags:["c#",".Net"]},{title:"Пятая группа",tags:["c#",".Net"]}]:[]};var G=Object(d.b)({users:function(){return Object(d.a)(f,function(e){return e.get("users")})}(),groups:function(){return Object(d.a)(f,function(e){return e.get("groups")})}()}),F=Object(u.b)(G,r),D=Object(m.a)({key:"header",reducer:S}),J=Object(g.a)({key:"header",saga:k.a});t.default=Object(p.c)(D,J,F)(H)},"./app/containers/Header/saga.js":function(e,t,n){"use strict";(function(e){function o(e){var t;return regeneratorRuntime.wrap(function(n){for(;;)switch(n.prev=n.next){case 0:return n.prev=0,n.next=3,Object(l.a)(a,e.name);case 3:return t=n.sent,n.next=6,Object(l.b)(Object(u.f)(t.users));case 6:n.next=12;break;case 8:return n.prev=8,n.t0=n.catch(0),n.next=12,Object(l.b)(Object(u.e)(n.t0));case 12:case"end":return n.stop()}},p,this,[[0,8]])}function a(t){return e(d.a.API_BASE_URL+"/users/search/"+t).then(function(e){return e.json()}).then(function(e){return e}).catch(function(e){return e})}function i(e){var t;return regeneratorRuntime.wrap(function(n){for(;;)switch(n.prev=n.next){case 0:return n.prev=0,n.next=3,Object(l.a)(s,e.title);case 3:return t=n.sent,n.next=6,Object(l.b)(Object(u.c)(t));case 6:n.next=12;break;case 8:return n.prev=8,n.t0=n.catch(0),n.next=12,Object(l.b)(Object(u.b)(n.t0));case 12:case"end":return n.stop()}},g,this,[[0,8]])}function s(t){return e(d.a.API_BASE_URL+"/group/search?type=Default&formed=false&minPrice=0&maxPrice=10000"+(""!==t?"&title="+t:"")).then(function(e){return e.json()}).then(function(e){return e}).catch(function(e){return e})}function r(){return regeneratorRuntime.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:return e.next=2,Object(l.c)(c.e,o);case 2:return e.next=4,Object(l.c)(c.b,i);case 4:case"end":return e.stop()}},m,this)}t.a=r;var l=n("./node_modules/redux-saga/es/effects.js"),c=n("./app/containers/Header/constants.js"),u=n("./app/containers/Header/actions.js"),d=n("./app/config.js"),p=regeneratorRuntime.mark(o),g=regeneratorRuntime.mark(i),m=regeneratorRuntime.mark(r)}).call(t,n("./node_modules/exports-loader/index.js?self.fetch!./node_modules/whatwg-fetch/fetch.js"))},"./app/images/logout.svg":function(e,t,n){e.exports=n.p+"6e32f1de273f7d843aab0d3725e40fa1.svg"},"./app/images/menu-white.svg":function(e,t,n){e.exports=n.p+"c2437546b6a2c941022dee55962a0f0b.svg"},"./app/images/notification.svg":function(e,t,n){e.exports=n.p+"46e08602812b87384f1b4ce94b1a0cf9.svg"},"./app/images/right-arrow.svg":function(e,t,n){e.exports=n.p+"6b2049e3755170c75b03e465619bab08.svg"}});