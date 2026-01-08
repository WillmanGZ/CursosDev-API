export interface Course {
  id: string;
  title: string;
  status: 'Draft' | 'Published';
  isDeleted: boolean;
  createdAt: string;
  updatedAt: string;
}

export interface CreateCourseRequest {
  title: string;
}

export interface UpdateCourseRequest {
  id: string;
  title: string;
}
