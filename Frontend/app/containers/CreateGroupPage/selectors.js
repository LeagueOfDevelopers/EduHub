import { createSelector } from 'reselect';

/**
 * Direct selector to the createGroupPage state domain
 */
const selectCreateGroupPage = (state) => state.get('createGroupPage');

const makeSelectTags = () => createSelector(
  selectCreateGroupPage,
  (createGroupPageState) => createGroupPageState.get('tags')
);

export {
  selectCreateGroupPage,
  makeSelectTags
};
