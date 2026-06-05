import { useQuery, useMutation } from '@tanstack/react-query';
import api from './client';
import type {
  Profile,
  Project,
  Skill,
  Experience,
  Education,
  Certification,
  BlogPostsResponse,
  BlogPostDetail,
  ContactRequest,
  ContactResponse,
} from './types';

export function useProfile() {
  return useQuery<Profile>({
    queryKey: ['profile'],
    queryFn: async () => {
      const { data } = await api.get<Profile>('/profile');
      return data;
    },
  });
}

export function useProjects(featured?: boolean) {
  return useQuery<Project[]>({
    queryKey: ['projects', { featured }],
    queryFn: async () => {
      const { data } = await api.get<Project[]>('/projects', { params: { featured } });
      return data;
    },
  });
}

export function useProject(id: string) {
  return useQuery<Project>({
    queryKey: ['projects', id],
    queryFn: async () => {
      const { data } = await api.get<Project>(`/projects/${id}`);
      return data;
    },
    enabled: !!id,
  });
}

export function useSkills(category?: string) {
  return useQuery<Skill[]>({
    queryKey: ['skills', { category }],
    queryFn: async () => {
      const { data } = await api.get<Skill[]>('/skills', { params: { category } });
      return data;
    },
  });
}

export function useExperiences() {
  return useQuery<Experience[]>({
    queryKey: ['experiences'],
    queryFn: async () => {
      const { data } = await api.get<Experience[]>('/experiences');
      return data;
    },
  });
}

export function useEducation() {
  return useQuery<Education[]>({
    queryKey: ['education'],
    queryFn: async () => {
      const { data } = await api.get<Education[]>('/education');
      return data;
    },
  });
}

export function useCertifications() {
  return useQuery<Certification[]>({
    queryKey: ['certifications'],
    queryFn: async () => {
      const { data } = await api.get<Certification[]>('/certifications');
      return data;
    },
  });
}

export function useBlogPosts(page: number = 1, pageSize: number = 10) {
  return useQuery<BlogPostsResponse>({
    queryKey: ['blog', { page, pageSize }],
    queryFn: async () => {
      const { data } = await api.get<BlogPostsResponse>('/blog', { params: { page, pageSize } });
      return data;
    },
  });
}

export function useBlogPost(slug: string) {
  return useQuery<BlogPostDetail>({
    queryKey: ['blog', slug],
    queryFn: async () => {
      const { data } = await api.get<BlogPostDetail>(`/blog/${slug}`);
      return data;
    },
    enabled: !!slug,
  });
}

export function useSendContactMessage() {
  return useMutation<ContactResponse, Error, ContactRequest>({
    mutationFn: async (payload) => {
      const { data } = await api.post<ContactResponse>('/contact', payload);
      return data;
    },
  });
}
