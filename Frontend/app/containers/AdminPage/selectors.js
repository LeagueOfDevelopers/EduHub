import { createSelector } from 'reselect';

/**
 * Direct selector to the adminPage state domain
 */
const selectAdminPageDomain = (state) => state.get('adminPage');



const makeSelectAdminPage = () => createSelector(
  selectAdminPageDomain,
  (substate) => substate.toJS()
);

export {
  selectAdminPageDomain,
};
