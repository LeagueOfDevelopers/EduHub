webpackJsonp([8],{"./app/components/MembersList/index.js":function(e,o,r){"use strict";function t(e,o){if(!(e instanceof o))throw new TypeError("Cannot call a class as a function")}function n(e,o){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!o||"object"!=typeof o&&"function"!=typeof o?e:o}function i(e,o){if("function"!=typeof o&&null!==o)throw new TypeError("Super expression must either be null or a function, not "+typeof o);e.prototype=Object.create(o&&o.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),o&&(Object.setPrototypeOf?Object.setPrototypeOf(e,o):e.__proto__=o)}function s(e){return{leaveGroup:function(o,r,t){return e(Object(b.B)(o,r,t))}}}Object.defineProperty(o,"__esModule",{value:!0});var a=r("./node_modules/react/react.js"),u=r.n(a),l=r("./node_modules/prop-types/index.js"),p=(r.n(l),r("./node_modules/react-redux/es/index.js")),c=r("./app/globalJS.js"),f=r("./node_modules/react-router-dom/index.js"),d=(r.n(f),r("./node_modules/reselect/es/index.js")),b=r("./app/containers/GroupPage/actions.js"),m=r("./node_modules/antd/lib/index.js"),y=(r.n(m),function(){var e="function"==typeof Symbol&&Symbol.for&&Symbol.for("react.element")||60103;return function(o,r,t,n){var i=o&&o.defaultProps,s=arguments.length-3;if(r||0===s||(r={}),r&&i)for(var a in i)void 0===r[a]&&(r[a]=i[a]);else r||(r=i||{});if(1===s)r.children=n;else if(s>1){for(var u=Array(s),l=0;l<s;l++)u[l]=arguments[l+3];r.children=u}return{$$typeof:e,type:o,key:void 0===t?null:""+t,ref:null,props:r,_owner:null}}}()),v=function(){function e(e,o){for(var r=0;r<o.length;r++){var t=o[r];t.enumerable=t.enumerable||!1,t.configurable=!0,"value"in t&&(t.writable=!0),Object.defineProperty(e,t.key,t)}}return function(o,r,t){return r&&e(o.prototype,r),t&&e(o,t),o}}(),x=y(m.Col,{},void 0,"Участников"),h=function(e){function o(e){t(this,o);var r=n(this,(o.__proto__||Object.getPrototypeOf(o)).call(this,e));return r.confirm=function(){m.message.error("Участник удален")},r.confirm=r.confirm.bind(r),r}return i(o,e),v(o,[{key:"render",value:function(){var e=this;return y("div",{className:"group-member-list",style:{boxShadow:"rgba(0, 0, 0, 0.4) 0px 0px 6px -2px"}},void 0,y(m.Row,{type:"flex",justify:"space-between",style:{padding:"6px 16px",boxShadow:"0px 2px 6px -2px rgba(0,0,0,0.36)"}},void 0,x,y(m.Col,{},void 0,this.props.memberAmount+"/"+this.props.size)),y("div",{className:"member-container"},void 0,y(m.List,{dataSource:this.props.members,renderItem:function(o){return y(m.List.Item,{},o.userId,y(m.List.Item.Meta,{avatar:y(m.Avatar,{src:o.avatarLink}),title:y(f.Link,{to:"/profile/"+o.userId},void 0,o.name),description:Object(c.c)(o.role)}),e.props.isCreator&&2!==o.role?y(m.Popconfirm,{title:"Удалить участника?",onConfirm:function(){return e.props.leaveGroup(e.props.groupId,o.userId,3===o.role?"Teacher":1===o.role?"Member":null)},okText:"Да",cancelText:"Нет"},void 0,y(m.Icon,{style:{fontSize:18,cursor:"pointer"},type:"close"})):null)}},void 0)))}}]),o}(u.a.Component),j=Object(d.b)({});o.default=Object(p.b)(j,s)(h)}});