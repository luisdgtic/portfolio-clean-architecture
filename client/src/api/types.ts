export interface Profile {
  fullName: string;
  title: string;
  summary: string;
  photoUrl: string | null;
  resumeUrl: string | null;
  email: string | null;
  phone: string | null;
  location: string | null;
  linkedInUrl: string | null;
  gitHubUrl: string | null;
  twitterUrl: string | null;
  websiteUrl: string | null;
}

export interface Project {
  id: string;
  title: string;
  description: string;
  imageUrls: string[];
  gitHubUrl: string | null;
  liveUrl: string | null;
  techStack: string[];
  isFeatured: boolean;
  sortOrder: number;
}

export interface Skill {
  id: string;
  name: string;
  category: string;
  proficiency: number;
  iconUrl: string | null;
  sortOrder: number;
}

export interface Experience {
  id: string;
  company: string;
  position: string;
  description: string | null;
  startDate: string;
  endDate: string | null;
  isCurrent: boolean;
  technologies: string[];
  companyUrl: string | null;
  sortOrder: number;
}

export interface Education {
  id: string;
  institution: string;
  degree: string;
  fieldOfStudy: string | null;
  startDate: string;
  endDate: string | null;
  isCurrent: boolean;
  gpa: number | null;
  description: string | null;
  sortOrder: number;
}

export interface Certification {
  id: string;
  name: string;
  issuer: string;
  issueDate: string;
  expiryDate: string | null;
  url: string | null;
  credentialId: string | null;
  sortOrder: number;
}

export interface BlogPost {
  id: string;
  title: string;
  slug: string;
  summary: string;
  publishedAt: string;
  tags: string[];
  readTimeMinutes: number;
}

export interface BlogPostDetail extends BlogPost {
  content: string;
}

export interface BlogPostsResponse {
  posts: BlogPost[];
  totalCount: number;
  page: number;
  pageSize: number;
}

export interface ContactRequest {
  name: string;
  email: string;
  subject: string;
  body: string;
}

export interface ContactResponse {
  id: string;
  message: string;
}
