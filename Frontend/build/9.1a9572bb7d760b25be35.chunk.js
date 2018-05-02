webpackJsonp([9],{"./app/components/MembersList/index.js":function(e,r,o){"use strict";function t(e,r){if(!(e instanceof r))throw new TypeError("Cannot call a class as a function")}function n(e,r){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!r||"object"!=typeof r&&"function"!=typeof r?e:r}function i(e,r){if("function"!=typeof r&&null!==r)throw new TypeError("Super expression must either be null or a function, not "+typeof r);e.prototype=Object.create(r&&r.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),r&&(Object.setPrototypeOf?Object.setPrototypeOf(e,r):e.__proto__=r)}function a(e){return{leaveGroup:function(r,o,t){return e(Object(m._2)(r,o,t))}}}Object.defineProperty(r,"__esModule",{value:!0});var s=o("./node_modules/react/react.js"),l=o.n(s),p=o("./node_modules/prop-types/index.js"),u=(o.n(p),o("./node_modules/react-redux/es/index.js")),c=o("./app/globalJS.js"),d=o("./node_modules/react-router-dom/index.js"),f=(o.n(d),o("./node_modules/reselect/es/index.js")),m=o("./app/containers/GroupPage/actions.js"),b=o("./node_modules/antd/lib/index.js"),v=(o.n(b),o("./app/config.js")),y=function(){var e="function"==typeof Symbol&&Symbol.for&&Symbol.for("react.element")||60103;return function(r,o,t,n){var i=r&&r.defaultProps,a=arguments.length-3;if(o||0===a||(o={}),o&&i)for(var s in i)void 0===o[s]&&(o[s]=i[s]);else o||(o=i||{});if(1===a)o.children=n;else if(a>1){for(var l=Array(a),p=0;p<a;p++)l[p]=arguments[p+3];o.children=l}return{$$typeof:e,type:r,key:void 0===t?null:""+t,ref:null,props:o,_owner:null}}}(),x=function(){function e(e,r){for(var o=0;o<r.length;o++){var t=r[o];t.enumerable=t.enumerable||!1,t.configurable=!0,"value"in t&&(t.writable=!0),Object.defineProperty(e,t.key,t)}}return function(r,o,t){return o&&e(r.prototype,o),t&&e(r,t),r}}(),h=y(b.Col,{},void 0,"Преподаватель"),j=y(b.Col,{},void 0,"Участники"),_=function(e){function r(e){t(this,r);var o=n(this,(r.__proto__||Object.getPrototypeOf(r)).call(this,e));return o.confirm=function(){b.message.error("Участник удален")},o.confirm=o.confirm.bind(o),o}return i(r,e),x(r,[{key:"render",value:function(){var e=this;return y("div",{className:"group-member-list",style:{boxShadow:"rgba(0, 0, 0, 0.4) 0px 1px 8px -2px"}},void 0,this.props.members.find(function(e){return 3===e.role})?y("div",{},void 0,y(b.Row,{type:"flex",justify:"space-between",style:{padding:"6px 16px",boxShadow:"0px 2px 6px -2px rgba(0,0,0,0.36)"}},void 0,h),y("div",{className:"teacher-container"},void 0,y(b.List,{dataSource:this.props.members.filter(function(e){return 3===e.role}),renderItem:function(r){return y(b.List.Item,{},r.userId,y(b.List.Item.Meta,{avatar:y(b.Avatar,{src:r.avatarLink?v.a.API_BASE_URL+"/file/img/"+r.avatarLink:null}),title:y(d.Link,{to:"/profile/"+r.userId},void 0,r.name),description:Object(c.e)(r.role)}),e.props.isCreator&&2!==r.role?y(b.Popconfirm,{title:"Удалить участника?",onConfirm:function(){return e.props.leaveGroup(e.props.groupId,r.userId,3===r.role?"Teacher":1===r.role?"Member":null)},okText:"Да",cancelText:"Нет"},void 0,y(b.Icon,{style:{fontSize:18,cursor:"pointer"},type:"close"})):null)}},void 0))):null,y("div",{},void 0,y(b.Row,{type:"flex",justify:"space-between",style:{padding:"6px 16px",boxShadow:"0px 1px 8px -2px rgba(0,0,0,0.36)"}},void 0,j,y(b.Col,{},void 0,this.props.memberAmount+"/"+this.props.size)),y("div",{className:"member-container"},void 0,y(b.List,{dataSource:this.props.members.filter(function(e){return 3!==e.role}),renderItem:function(r){return y(b.List.Item,{},r.userId,y(b.List.Item.Meta,{avatar:y(b.Avatar,{src:r.avatarLink?v.a.API_BASE_URL+"/file/img/"+r.avatarLink:null}),title:y(d.Link,{to:"/profile/"+r.userId},void 0,r.name),description:Object(c.e)(r.role)}),e.props.isCreator&&2!==r.role?y(b.Popconfirm,{title:"Удалить участника?",onConfirm:function(){return e.props.leaveGroup(e.props.groupId,r.userId,3===r.role?"Teacher":1===r.role?"Member":null)},okText:"Да",cancelText:"Нет"},void 0,y(b.Icon,{style:{fontSize:18,cursor:"pointer"},type:"close"})):null)}},void 0))))}}]),r}(l.a.Component),g=Object(f.b)({});r.default=Object(u.b)(g,a)(_)}});