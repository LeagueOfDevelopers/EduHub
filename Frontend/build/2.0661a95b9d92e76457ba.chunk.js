webpackJsonp([2],{"./app/containers/Header/index.js":function(e,t,n){"use strict";Object.defineProperty(t,"__esModule",{value:!0}),function(e){function o(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}function a(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}function i(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}function r(e){return{dispatch:e}}var s=n("./node_modules/react/react.js"),l=n.n(s),c=n("./node_modules/prop-types/index.js"),u=(n.n(c),n("./node_modules/react-redux/es/index.js")),d=n("./node_modules/reselect/es/index.js"),p=n("./node_modules/redux/es/index.js"),f=n("./app/utils/injectSaga.js"),m=n("./app/utils/injectReducer.js"),h=n("./app/containers/Header/selectors.js"),g=n("./app/containers/Header/reducer.js"),v=n("./app/containers/Header/saga.js"),y=n("./node_modules/styled-components/dist/styled-components.es.js"),b=n("./app/config.js"),j=n("./app/globalJS.js"),S=n("./node_modules/react-router-dom/index.js"),k=(n.n(S),n("./app/containers/SigningInForm/index.js")),C=n("./node_modules/antd/lib/index.js"),w=(n.n(C),function(){var e="function"==typeof Symbol&&Symbol.for&&Symbol.for("react.element")||60103;return function(t,n,o,a){var i=t&&t.defaultProps,r=arguments.length-3;if(n||0===r||(n={}),n&&i)for(var s in i)void 0===n[s]&&(n[s]=i[s]);else n||(n=i||{});if(1===r)n.children=a;else if(r>1){for(var l=Array(r),c=0;c<r;c++)l[c]=arguments[c+3];n.children=l}return{$$typeof:e,type:t,key:void 0===o?null:""+o,ref:null,props:n,_owner:null}}}()),I=function(){function e(e,t){for(var n=0;n<t.length;n++){var o=t[n];o.enumerable=o.enumerable||!1,o.configurable=!0,"value"in o&&(o.writable=!0),Object.defineProperty(e,o.key,o)}}return function(t,n,o){return n&&e(t.prototype,n),o&&e(t,o),t}}(),_=C.Select.Option,x=C.Select.OptGroup,O=y.a.div.withConfig({displayName:"Header__Logo"})(["font-size: 36px;cursor: pointer;"]),N=w("div",{},void 0,"Мой аккаунт"),D=w("div",{},void 0,"Уведомления"),L=w(C.Menu.Item,{className:"unhover"},"1",w(S.Link,{className:"profile",to:"/registration"},void 0,w(C.Button,{type:"primary",htmlType:"button"},void 0,"Зарегистрироваться"))),T=w(O,{},void 0,"EduHub"),H=w(C.Col,{span:12},void 0,"Пользователи"),A=w(S.Link,{to:"#"},void 0,"Показать больше"),V=w("div",{},void 0,"Пользователи"),P=w(C.Col,{span:12},void 0,"Группы"),R=w(S.Link,{to:"#"},void 0,"Показать больше"),M=w("div",{},void 0,"Группы"),J=w(S.Link,{className:"profile",to:"/registration"},void 0,w(C.Button,{type:"primary",htmlType:"submit"},void 0,"Зарегистрироваться")),E=function(t){function r(t){o(this,r);var n=a(this,(r.__proto__||Object.getPrototypeOf(r)).call(this,t));return n.onSignInClick=function(){n.setState({signInVisible:!0})},n.handleCancel=function(){n.setState({signInVisible:!1})},n.logout=function(){localStorage.setItem("name",""),localStorage.setItem("avatarLink",""),localStorage.setItem("token",""),location.assign("/")},n.acc_menu=w(C.Menu,{},void 0,w(C.Menu.Item,{className:"menu-item"},"0",localStorage.getItem("token")?w(S.Link,{to:"/profile/"+Object(j.c)(localStorage.getItem("token")).UserId},void 0,N):null),w(C.Menu.Item,{className:"menu-item"},"1",localStorage.getItem("token")?w(S.Link,{to:"/profile/"+Object(j.c)(localStorage.getItem("token")).UserId+"/notifications"},void 0,D):null),w(C.Menu.Item,{className:"danger-menu-item menu-item"},"2",w("div",{style:{color:"red"},onClick:n.logout},void 0,"Выйти"))),n.menu=w(C.Menu,{},void 0,w(C.Menu.Item,{className:"unhover"},"0",w(C.Button,{className:"profile",style:{width:"100%"},htmlType:"button",onClick:n.onSignInClick},void 0,"Войти")),L),n.defaultSelectData={users:["Первый пользователь","Второй пользователь","Третий пользователь","Четвертый пользователь","Пятый пользователь"],groups:[{title:"Первая группа",tags:["js","c#"]},{title:"Вторая группа",tags:["js","react"]},{title:"Третья группа",tags:["c#",".Net"]},{title:"Четвертая группа",tags:["c#",".Net"]},{title:"Пятая группа",tags:["c#",".Net"]}]},n.fetchData=function(t,o){var a=null;"true"===localStorage.getItem("without_server")?o(n.defaultSelectData):e(b.a.API_BASE_URL+"/users/search",{method:"POST",headers:{"Content-Type":"application/json-patch+json"},body:JSON.stringify({name:t})}).then(function(e){return e.json()}).then(function(e){return a=e}).catch(function(e){return e}),setTimeout(function(){return o({users:a.users&&""!==n.state.searchValue?a.users:[],groups:[]})},1e3)},n.handleSelectChange=function(e){n.setState({searchValue:e}),""!==e?n.fetchData(n.state.searchValue,function(e){return n.setState({searchData:e})}):n.setState({searchData:{users:[],groups:[]}})},n.state={signInVisible:!1,searchValue:"",searchData:{users:[],groups:[]}},n.logout=n.logout.bind(n),n.onSignInClick=n.onSignInClick.bind(n),n.handleCancel=n.handleCancel.bind(n),n.handleSelectChange=n.handleSelectChange.bind(n),n.fetchData=n.fetchData.bind(n),n}return i(r,t),I(r,[{key:"render",value:function(){var e=this;return w(C.Row,{type:"flex",align:"middle",className:"header",style:{width:"100hr"}},void 0,w(C.Col,{span:2,offset:2},void 0,w("div",{style:{width:80}},void 0,w(S.Link,{to:"/",style:{color:"rgba(0,0,0,0.65)",textDecoration:"none"}},void 0,T))),w(C.Col,{span:6,offset:2,style:{position:"relative"}},void 0,w(C.Select,{mode:"combobox",className:"search",style:{width:"100%"},value:this.state.searchValue,placeholder:"Поиск",size:"large",defaultActiveFirstOption:!1,showArrow:!1,onChange:this.handleSelectChange,onSelect:function(){setTimeout(function(){return e.setState({searchValue:""})},0)}},void 0,w(x,{label:this.state.searchData.users.length>4?w("div",{},void 0,H,w(C.Col,{span:12,style:{textAlign:"right"}},void 0,A)):V},"1",this.state.searchData.users.map(function(e,t){return t<4?w(_,{className:"search-option-item"},e.name,w(S.Link,{className:"search-user-link",to:"/profile/"+e.id},void 0,w("div",{},void 0,e.name))):null})),w(x,{label:this.state.searchData.groups.length>4?w("div",{},void 0,P,w(C.Col,{span:12,style:{textAlign:"right"}},void 0,R)):M},"2",this.state.searchData.groups.map(function(e,t){return t<4?w(_,{},e.title,w("div",{},void 0,e.title),w("div",{},void 0,e.tags.map(function(e){return w(S.Link,{to:"",style:{marginRight:6}},void 0,e)}))):null}))),w(C.Icon,{type:"search",className:"search",style:{fontSize:20,position:"absolute",top:10,right:10,opacity:.8}})),localStorage.getItem("token")?w(C.Col,{span:4,offset:6,style:{display:"flex",justifyContent:"flex-end"}},void 0,w(C.Dropdown,{overlay:this.acc_menu,trigger:["click"]},void 0,w("div",{style:{display:"flex",justifyContent:"flex-end",alignItems:"center",marginLeft:"36%"}},void 0,w(C.Avatar,{src:localStorage.getItem("avatarLink"),size:"large",style:{backgroundColor:"#fff",color:"rgba(0,0,0,0.65)",minHeight:40,minWidth:40,marginRight:10,cursor:"pointer"}}),w("span",{className:"userName",style:{whiteSpace:"nowrap",cursor:"pointer"}},void 0,localStorage.getItem("name"))))):w(C.Col,{span:6,offset:4,style:{display:"flex",justifyContent:"flex-end"}},void 0,w(C.Button,{className:"profile",htmlType:"button",onClick:this.onSignInClick,style:{marginRight:"6%"}},void 0,"Войти"),w(k.a,{visible:this.state.signInVisible,handleCancel:this.handleCancel}),J,w(C.Dropdown,{className:"unregistered-person",overlay:this.menu,trigger:["click"]},void 0,w("img",{className:"menu-btn",style:{width:26,cursor:"pointer"},src:n("./app/images/menu.svg"),alt:""}))))}}]),r}(l.a.PureComponent),B=Object(d.b)({header:Object(h.a)()}),z=Object(u.b)(B,r),U=Object(m.a)({key:"header",reducer:g.a}),F=Object(f.a)({key:"header",saga:v.a});t.default=Object(p.c)(U,F,z)(E)}.call(t,n("./node_modules/exports-loader/index.js?self.fetch!./node_modules/whatwg-fetch/fetch.js"))},"./app/containers/Header/reducer.js":function(e,t,n){"use strict";function o(){var e=arguments.length>0&&void 0!==arguments[0]?arguments[0]:r;switch(arguments[1].type){case i:default:return e}}var a=n("./node_modules/immutable/dist/immutable.js"),i="app/Header/DEFAULT_ACTION",r=Object(a.fromJS)({});t.a=o},"./app/containers/Header/saga.js":function(e,t,n){"use strict";function o(){return regeneratorRuntime.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:case"end":return e.stop()}},a,this)}t.a=o;var a=regeneratorRuntime.mark(o)},"./app/containers/Header/selectors.js":function(e,t,n){"use strict";var o=n("./node_modules/reselect/es/index.js"),a=function(e){return e.get("header")},i=function(){return Object(o.a)(a,function(e){return e.toJS()})};t.a=i},"./app/globalJS.js":function(e,t,n){"use strict";function o(e){var t=e.split(".")[1],n=t.replace("-","+").replace("_","/");return JSON.parse(window.atob(n))}function a(e){switch(e){case 0:return"Обычный пользователь";case 1:return"Участник";case 2:return"Учитель";case 3:return"Создатель"}}function i(e){switch(e){case 0:return"Лекция";case 1:return"Семинар";case 2:return"Мастер-класс"}}t.c=o,t.b=a,t.a=i},"./app/images/menu.svg":function(e,t,n){e.exports=n.p+"91f551a1ed6bf78427aa2d5ef0420925.svg"}});