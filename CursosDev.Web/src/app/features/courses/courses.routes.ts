import { Routes } from '@angular/router';
import { authGuard } from '../../core/auth';

export const COURSES_ROUTES: Routes = [
  {
    path: '',
    canActivate: [authGuard],
    loadComponent: () =>
      import('./course-list/course-list.component').then((m) => m.CourseListComponent),
  },
  {
    path: 'new',
    canActivate: [authGuard],
    loadComponent: () =>
      import('./course-form/course-form.component').then((m) => m.CourseFormComponent),
  },
  {
    path: ':id',
    canActivate: [authGuard],
    loadComponent: () =>
      import('./course-form/course-form.component').then((m) => m.CourseFormComponent),
  },
];
