webpackJsonp([3],{"./app/containers/HomePage/actions.js":function(e,t,n){"use strict";n.d(t,"d",function(){return o}),n.d(t,"f",function(){return s}),n.d(t,"e",function(){return a}),n.d(t,"a",function(){return i}),n.d(t,"c",function(){return u}),n.d(t,"b",function(){return c});var r=n("./app/containers/HomePage/constants.js"),o=function(){return{type:r.e}},s=function(e){return{type:r.f,payload:e}},a=function(e){return{type:r.d,payload:e}},i=function(){return{type:r.b}},u=function(e){return{type:r.c,payload:e}},c=function(e){return{type:r.a,payload:e}}},"./app/containers/HomePage/constants.js":function(e,t,n){"use strict";n.d(t,"e",function(){return r}),n.d(t,"f",function(){return o}),n.d(t,"d",function(){return s}),n.d(t,"b",function(){return a}),n.d(t,"c",function(){return i}),n.d(t,"a",function(){return u});var r="GET_UNASSEMBLED_GROUPS_START",o="GET_UNASSEMBLED_GROUPS_SUCCESS",s="GET_UNASSEMBLED_GROUPS_ERROR",a="GET_ASSEMBLED_GROUPS_START",i="GET_ASSEMBLED_GROUPS_SUCCESS",u="GET_ASSEMBLED_GROUPS_ERROR"},"./app/containers/HomePage/index.js":function(e,t,n){"use strict";function r(){var e=arguments.length>0&&void 0!==arguments[0]?arguments[0]:_,t=arguments[1];switch(t.type){case j.b:return e.set("pending",!0).set("error",!1);case j.c:return e.set("pending",!1).set("assembledGroups",t.payload);case j.a:return e.set("pending",!1).set("error",t.payload);case j.e:return e.set("pending",!0).set("error",!1);case j.f:return e.set("pending",!1).set("unassembledGroups",t.payload);case j.d:return e.set("pending",!1).set("error",t.payload);default:return e}}function o(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}function s(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}function a(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}function i(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}function u(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}function c(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}function p(e){return{getUnassembledGroups:function(){return e(Object(w.d)())},getAssembledGroups:function(){return e(Object(w.a)())}}}Object.defineProperty(t,"__esModule",{value:!0});var l=n("./node_modules/react/react.js"),d=n.n(l),f=(n("./node_modules/prop-types/index.js"),n("./node_modules/react-redux/es/index.js")),m=n("./node_modules/reselect/es/index.js"),b=n("./node_modules/redux/es/index.js"),g=n("./app/utils/injectSaga.js"),y=n("./app/utils/injectReducer.js"),v=function(e){return e.get("homePage")},h=n("./node_modules/immutable/dist/immutable.js"),j=n("./app/containers/HomePage/constants.js"),_=Object(h.fromJS)({unassembledGroups:[],assembledGroups:[],pending:!1,error:!1}),O=r,S=n("./app/containers/HomePage/saga.js"),w=n("./app/containers/HomePage/actions.js"),P=n("./node_modules/react-router-dom/index.js"),x=n("./node_modules/antd/lib/index.js"),E=n("./app/components/UnassembledGroupCard/index.js"),R=n("./app/globalJS.js"),k=function(){var e="function"==typeof Symbol&&Symbol.for&&Symbol.for("react.element")||60103;return function(t,n,r,o){var s=t&&t.defaultProps,a=arguments.length-3;if(n||0===a||(n={}),n&&s)for(var i in s)void 0===n[i]&&(n[i]=s[i]);else n||(n=s||{});if(1===a)n.children=o;else if(a>1){for(var u=Array(a),c=0;c<a;c++)u[c]=arguments[c+3];n.children=u}return{$$typeof:e,type:t,key:void 0===r?null:""+r,ref:null,props:n,_owner:null}}}(),C=function(){function e(e,t){for(var n=0;n<t.length;n++){var r=t[n];r.enumerable=r.enumerable||!1,r.configurable=!0,"value"in r&&(r.writable=!0),Object.defineProperty(e,r.key,r)}}return function(t,n,r){return n&&e(t.prototype,n),r&&e(t,r),t}}(),G=k(x.Col,{},void 0,"Участников"),I=k(x.Col,{},void 0,"Оплата"),T=k(x.Col,{},void 0,"Тип"),U=function(e){function t(e){return o(this,t),s(this,(t.__proto__||Object.getPrototypeOf(t)).call(this,e))}return a(t,e),C(t,[{key:"render",value:function(){return k(x.Col,{},void 0,k(x.Card,{title:this.props.groupInfo.title,hoverable:!0,className:"group-card"},void 0,k(x.Row,{type:"flex",justify:"space-between",style:{marginBottom:8}},void 0,G,k(x.Col,{},void 0,this.props.numberOfMembers+"/"+this.props.groupInfo.size)),k(x.Row,{type:"flex",justify:"space-between",style:{marginBottom:8}},void 0,I,k(x.Col,{},void 0,this.props.groupInfo.moneyPerUser," руб.")),k(x.Row,{type:"flex",justify:"space-between",style:{marginBottom:10}},void 0,T,k(x.Col,{},void 0,Object(R.a)(this.props.groupInfo.groupType))),k(x.Row,{gutter:6,type:"flex",justify:"start"},void 0,this.props.groupInfo.tags.map(function(e){return k(P.Link,{to:"#"},e,e)}))))}}]),t}(d.a.PureComponent);U.defaultProps={groupInfo:{title:"",numberOfMembers:0,size:0,moneyPerUser:0,groupType:"",tags:[]}};var L=U,A=n("./app/containers/SigningInForm/index.js");n.d(t,"HomePage",function(){return $});var B=function(){var e="function"==typeof Symbol&&Symbol.for&&Symbol.for("react.element")||60103;return function(t,n,r,o){var s=t&&t.defaultProps,a=arguments.length-3;if(n||0===a||(n={}),n&&s)for(var i in s)void 0===n[i]&&(n[i]=s[i]);else n||(n=s||{});if(1===a)n.children=o;else if(a>1){for(var u=Array(a),c=0;c<a;c++)u[c]=arguments[c+3];n.children=u}return{$$typeof:e,type:t,key:void 0===r?null:""+r,ref:null,props:n,_owner:null}}}(),M=function(){function e(e,t){for(var n=0;n<t.length;n++){var r=t[n];r.enumerable=r.enumerable||!1,r.configurable=!0,"value"in r&&(r.writable=!0),Object.defineProperty(e,r.key,r)}}return function(t,n,r){return n&&e(t.prototype,n),r&&e(t,r),t}}(),H=[{groupInfo:{id:1,title:"cdcvvdsc",size:8,moneyPerUser:600,groupType:"Lfdsv",tags:["fds","sdf"]},numberOfMembers:6}],N=[{groupInfo:{id:2,title:"cdcvvdsc",size:8,moneyPerUser:600,groupType:"Lfdsv",tags:["fds","sdf"],description:"dadasddas"},numberOfMembers:6}],z=B(P.Link,{to:"/groups/unassembledGroups"},void 0,"Показать больше"),D=B(P.Link,{to:"/create_group"},void 0,B(x.Button,{type:"primary",htmlType:"submit"},void 0,"Создать группу")),V=B(P.Link,{to:"/groups/assembledGroups"},void 0,"Показать больше"),$=function(e){function t(e){i(this,t);var n=u(this,(t.__proto__||Object.getPrototypeOf(t)).call(this,e));return n.handleCancel=function(){n.setState({signInVisible:!1})},n.makeTeacher=n.makeTeacher.bind(n),n.handleCancel=n.handleCancel.bind(n),n.state={signInVisible:!1},n}return c(t,e),M(t,[{key:"componentDidMount",value:function(){"true"!==localStorage.getItem("without_server")&&(this.props.getUnassembledGroups(),this.props.getAssembledGroups())}},{key:"makeTeacher",value:function(){localStorage.getItem("token")?x.message.success("Теперь вы можете преподавать!"):this.setState({signInVisible:!0})}},{key:"render",value:function(){return B("div",{},void 0,B(x.Col,{span:20,offset:2,style:{marginTop:40}},void 0,B(x.Card,{title:"Незаполненные группы",bordered:!1,className:"unassembled-groups-list font-size-20",extra:z},void 0,"true"===localStorage.getItem("without_server")?B("div",{className:"cards-holder cards-holder-center"},void 0,H.map(function(e){return B(P.Link,{to:"/group/"+e.groupInfo.id},e.groupInfo.id,d.a.createElement(E.a,e))})):B("div",{className:"cards-holder cards-holder-center"},void 0,this.props.unassembledGroups.map(function(e){return B(P.Link,{to:"/group/"+e.groupInfo.id},e.groupInfo.id,d.a.createElement(E.a,e))})),B(x.Row,{type:"flex",justify:"end",align:"middle",style:{marginTop:30}},void 0,B(x.Col,{style:{fontSize:18,marginRight:"2%"}},void 0,"Не нашли то, что искали?"),D))),B(x.Col,{span:20,offset:2,style:{marginTop:40}},void 0,B(x.Card,{title:"Заполненные группы",bordered:!1,className:"assembled-groups-list font-size-20",extra:V},void 0,"true"===localStorage.getItem("without_server")?B("div",{className:"cards-holder cards-holder-center"},void 0,N.map(function(e){return B(P.Link,{to:"/group/"+e.groupInfo.id},e.groupInfo.id,d.a.createElement(L,e))})):B("div",{className:"cards-holder cards-holder-center"},void 0,this.props.assembledGroups.map(function(e){return B(P.Link,{to:"/group/"+e.groupInfo.id},e.groupInfo.id,d.a.createElement(L,e))})),B(x.Row,{type:"flex",justify:"end",align:"middle",style:{marginTop:30}},void 0,B(x.Col,{style:{fontSize:18,marginRight:"2%"}},void 0,"Уже знаете, чему будете учить?"),B(x.Button,{type:"primary",onClick:this.makeTeacher},void 0,"Стать преподавателем"),B(A.a,{visible:this.state.signInVisible,handleCancel:this.handleCancel})))))}}]),t}(d.a.PureComponent),J=Object(m.b)({unassembledGroups:function(){return Object(m.a)(v,function(e){return e.get("unassembledGroups")})}(),assembledGroups:function(){return Object(m.a)(v,function(e){return e.get("assembledGroups")})}()}),F=Object(f.b)(J,p),q=Object(y.a)({key:"homePage",reducer:O}),K=Object(g.a)({key:"homePage",saga:S.a});t.default=Object(b.c)(q,K,F)($)},"./app/containers/HomePage/saga.js":function(e,t,n){"use strict";(function(e){function r(){var e,t;return regeneratorRuntime.wrap(function(n){for(;;)switch(n.prev=n.next){case 0:return n.prev=0,n.next=3,Object(u.a)(s);case 3:return e=n.sent,t=e.groups,n.next=7,Object(u.b)(Object(p.f)(t));case 7:n.next=13;break;case 9:return n.prev=9,n.t0=n.catch(0),n.next=13,Object(u.b)(Object(p.e)(n.t0));case 13:case"end":return n.stop()}},d,this,[[0,9]])}function o(){var e,t;return regeneratorRuntime.wrap(function(n){for(;;)switch(n.prev=n.next){case 0:return n.prev=0,n.next=3,Object(u.a)(a);case 3:return e=n.sent,t=e.groups,n.next=7,Object(u.b)(Object(p.c)(t));case 7:n.next=13;break;case 9:return n.prev=9,n.t0=n.catch(0),n.next=13,Object(u.b)(Object(p.b)(n.t0));case 13:case"end":return n.stop()}},f,this,[[0,9]])}function s(){return e(c.a.API_BASE_URL+"/group").then(function(e){return e.json()}).catch(function(e){return e})}function a(){return e(c.a.API_BASE_URL+"/group").then(function(e){return e.json()}).catch(function(e){return e})}function i(){return regeneratorRuntime.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:return e.next=2,Object(u.c)(l.e,r);case 2:return e.next=4,Object(u.c)(l.b,o);case 4:case"end":return e.stop()}},m,this)}t.a=i;var u=n("./node_modules/redux-saga/es/effects.js"),c=n("./app/config.js"),p=n("./app/containers/HomePage/actions.js"),l=n("./app/containers/HomePage/constants.js"),d=regeneratorRuntime.mark(r),f=regeneratorRuntime.mark(o),m=regeneratorRuntime.mark(i)}).call(t,n("./node_modules/exports-loader/index.js?self.fetch!./node_modules/whatwg-fetch/fetch.js"))}});