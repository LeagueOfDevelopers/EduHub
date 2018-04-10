import { createSelector } from 'reselect';

/**
 * Direct selector to the groupPage state domain
 */
const selectGroupPage = (state) => state.get('groupPage');

const makeSelectUsers = () => createSelector(
  selectGroupPage,
  (groupPageState) => groupPageState.get('users')
);

const makeSelectNeedUpdate = () => createSelector(
  selectGroupPage,
  (groupPageState) => groupPageState.get('needUpdate')
);

const makeSelectPlan = () => createSelector(
  selectGroupPage,
  (groupPageState) => groupPageState.get('currentPlan')
);

const makeSelectChat = () => createSelector(
  selectGroupPage,
  (groupPageState) => groupPageState.get('chat')
);

const makeSelectTags = () => createSelector(
  selectGroupPage,
  (groupPageState) => groupPageState.get('tags')
);

export {
  makeSelectUsers,
  makeSelectNeedUpdate,
  makeSelectPlan,
  makeSelectChat,
  makeSelectTags
};
