import { createSelector } from 'reselect';

/**
 * Direct selector to the groupPage state domain
 */
const selectGroupPage = (state) => state.get('groupPage');

const makeSelectGroupData = () => createSelector(
  selectGroupPage,
  (groupState) => groupState.get('groupData')
);

export {
  selectGroupPage,
  makeSelectGroupData
};
